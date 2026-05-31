import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { PedidosViewModel } from '../../viewmodels/pedidos.viewmodels';
import type { PedidoDTO } from '../../../domain/dtos/PedidoDTO';

/**
 * Dialogo de confirmación cuando el usuario intenta cambiar de proveedor
 * con líneas de producto ya añadidas al pedido.
 * @internal
 */
@Component({
  selector: 'app-confirm-cambio-proveedor',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
  template: `
  <div class="cp-head">
    <div class="cp-icon">⚠️</div>
    <div class="cp-info">
      <div class="cp-title">Cambiar proveedor</div>
      <div class="cp-sub">Se perderán los productos añadidos</div>
    </div>
  </div>
  <div mat-dialog-content class="cp-body">
    <p>Si cambias de proveedor, <strong>la lista de productos del pedido se borrará</strong> completamente.</p>
    <p class="cp-warn">¿Deseas continuar igualmente?</p>
  </div>
  <div mat-dialog-actions align="end" class="cp-actions">
    <button mat-stroked-button (click)="ref.close(false)">Cancelar</button>
    <button mat-raised-button color="warn" (click)="ref.close(true)">
      <mat-icon>swap_horiz</mat-icon> Cambiar proveedor
    </button>
  </div>`,
  styles: [`.cp-head{display:flex;align-items:center;gap:14px;padding:20px 22px 16px;border-bottom:1px solid #fef3c7;background:#fffbeb;} .cp-icon{width:44px;height:44px;border-radius:12px;background:rgba(245,158,11,.12);display:grid;place-items:center;font-size:20px;flex-shrink:0;} .cp-info{flex:1;} .cp-title{font-size:16px;font-weight:800;color:#92400e;} .cp-sub{font-size:13px;color:#6b7280;margin-top:2px;} .cp-body{padding:16px 22px!important;} .cp-body p{font-size:14px;color:#374151;margin:0 0 8px;} .cp-warn{font-size:13px;color:#b45309!important;font-weight:600;} .cp-actions{padding:12px 22px!important;gap:8px;}`]
})
export class ConfirmCambioProveedorDialogComponent {
  constructor(public readonly ref: MatDialogRef<ConfirmCambioProveedorDialogComponent>) {}
}

/**
 * Formulario para crear o editar un pedido.
 * @remarks En modo edición carga los datos del pedido y sus líneas desde el backend.
 * El IVA está fijado al 21 % y no es editable por el usuario.
 */
@Component({
  selector: 'app-pedidos-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'es-ES' }
  ],
  templateUrl: './pedidos-form.html',
  styleUrls: ['./pedidos-form.scss']
})
export class PedidosFormComponent implements OnInit {
  private readonly fb     = inject(FormBuilder);
  private readonly route  = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly dialog = inject(MatDialog);
  public  readonly vm     = inject(PedidosViewModel);

  form!: FormGroup;
  isEditMode           = false;
  pedidoId?: number;
  numeroPedidoDisplay  = '';
  isLoadingPedido      = false;
  isLoadingProductos   = false;
  baseImponible        = 0;
  totalConIva          = 0;

  /** IVA fijo al 21 % — no lo selecciona el usuario. */
  private readonly IVA = 0.21;

  productosDelProveedor: any[] = [];

  /** ID del proveedor seleccionado antes del último cambio (para revertir si se cancela). */
  private proveedorAnterior: number | null = null;

  /** Inicializa el formulario; en modo edición carga el pedido existente. Suscribe al cambio de proveedor. */
  ngOnInit(): void {
    this.initForm();
    const idParam = this.route.snapshot.params['id'];
    if (idParam) {
      this.isEditMode = true;
      this.pedidoId   = Number(idParam);
      this.cargarDatosPedido(this.pedidoId);
    } else {
      this._agregarLinea();
      this.recalcularTotales();
    }

    this.form.get('proveedorId')!.valueChanges.subscribe((provId: number | null) => {
      const tieneProductos = this.lineas.controls.some(
        ctrl => ctrl.get('productoId')?.value != null
      );

      if (tieneProductos && this.proveedorAnterior !== null && provId !== null) {
        const ref = this.dialog.open(ConfirmCambioProveedorDialogComponent, { width: '440px' });
        ref.afterClosed().pipe(take(1)).subscribe(confirmado => {
          if (confirmado) {
            this.proveedorAnterior = provId;
            this.cargarProductosProveedor(provId);
          } else {
            // Revertir el select sin disparar valueChanges de nuevo
            this.form.get('proveedorId')!.setValue(this.proveedorAnterior, { emitEvent: false });
          }
        });
        return;
      }

      this.proveedorAnterior = provId;
      if (provId) {
        this.cargarProductosProveedor(provId);
      } else {
        this.limpiarLineas();
      }
    });
  }

  /** Construye el FormGroup inicial del formulario. */
  private initForm(): void {
    this.form = this.fb.group({
      numeroPedido:  [{ value: '', disabled: true }],
      proveedorId:   [null, Validators.required],
      fechaPedido:   [{ value: new Date(), disabled: true }],
      observaciones: [''],
      lineas:        this.fb.array([])
    });
  }

  /** Devuelve el FormArray de líneas de producto. */
  get lineas(): FormArray {
    return this.form.get('lineas') as FormArray;
  }

  /**
   * Calcula el subtotal de una línea (cantidad × precio unitario).
   * @param index Índice de la línea en el FormArray
   * @returns Importe de la línea o 0 si los valores no son finitos
   */
  getSubtotal(index: number): number {
    const row  = this.lineas.at(index);
    const cant = Number(row.get('cantidad')?.value       ?? 0);
    const prec = Number(row.get('precioUnitario')?.value ?? 0);
    const sub  = cant * prec;
    return Number.isFinite(sub) ? sub : 0;
  }

  /**
   * Carga los datos de un pedido existente en modo edición.
   * @param id ID del pedido a editar
   */
  private cargarDatosPedido(id: number): void {
    this.isLoadingPedido = true;
    this.vm.obtenerPedidoPorId(id).subscribe({
      next: (pedido: PedidoDTO | any) => {
        this.numeroPedidoDisplay = pedido.numeroPedido ?? '';
        const provId = pedido.proveedor?.proveedorID ?? (pedido as any).proveedorID ?? null;

        this.form.patchValue({
          numeroPedido:  pedido.numeroPedido ?? '',
          proveedorId:   provId,
          fechaPedido:   pedido.fechaPedido ? new Date(pedido.fechaPedido) : new Date(),
          observaciones: pedido.observaciones ?? ''
        }, { emitEvent: false });

        this.proveedorAnterior = provId;

        const poblarLineas = (lineasApi: any[]) => {
          this.lineas.clear();
          if (Array.isArray(lineasApi) && lineasApi.length > 0) {
            lineasApi.forEach((l: any) => this._agregarLinea(l));
          } else {
            this._agregarLinea();
          }
          this.recalcularTotales();
        };

        if (provId) {
          this.isLoadingProductos = true;
          this.vm.getProductosPorProveedor(provId).pipe(take(1)).subscribe({
            next: (data) => {
              this.productosDelProveedor = this.normalizarProductos(data ?? []);
              this.vm.setProductos(this.productosDelProveedor);
              this.isLoadingProductos = false;
              poblarLineas(pedido?.detalles ?? pedido?.lineas ?? []);
            },
            error: () => {
              this.isLoadingProductos = false;
              poblarLineas(pedido?.detalles ?? pedido?.lineas ?? []);
            }
          });
        } else {
          poblarLineas(pedido?.detalles ?? pedido?.lineas ?? []);
        }

        this.isLoadingPedido = false;
      },
      error: () => { this.isLoadingPedido = false; }
    });
  }

  /**
   * Limpia las líneas actuales y varía los selectores de producto a null.
   * @internal
   */
  private limpiarLineas(): void {
    this.productosDelProveedor = [];
    this.vm.setProductos([]);
    this.lineas.controls.forEach(ctrl => {
      ctrl.get('productoId')?.setValue(null, { emitEvent: false });
      ctrl.get('precioUnitario')?.setValue(0, { emitEvent: false });
      ctrl.get('subtotal')?.setValue(0, { emitEvent: false });
    });
    this.recalcularTotales();
  }

  /**
   * Limpia las líneas y carga los productos del nuevo proveedor.
   * @param provId ID del proveedor seleccionado
   * @internal
   */
  private cargarProductosProveedor(provId: number): void {
    this.limpiarLineas();
    this.isLoadingProductos = true;
    this.vm.getProductosPorProveedor(provId).pipe(take(1)).subscribe({
      next: (data) => {
        this.productosDelProveedor = this.normalizarProductos(data ?? []);
        this.vm.setProductos(this.productosDelProveedor);
        this.isLoadingProductos = false;
      },
      error: () => { this.isLoadingProductos = false; }
    });
  }

  /**
   * Normaliza la lista de productos de un proveedor resolviendo distintos
   * nombres de campo que puede devolver el backend.
   * @param data Array crudo recibido del backend
   * @returns Array con `nombreProducto` siempre presente
   * @internal
   */
  private normalizarProductos(data: any[]): any[] {
    return data.map((p: any) => ({
      ...p,
      nombreProducto:
        p.nombreProducto ??
        p.NombreProducto ??
        p.nombre         ??
        p.Nombre         ??
        p.productName    ??
        p.name           ??
        `[ID ${p.productoID ?? p.ProductoID}]`,
    }));
  }

  /**
   * Añade una nueva línea de producto al FormArray.
   * @param data Datos iniciales de la línea (usado en modo edición)
   */
  _agregarLinea(data?: any): void {
    const lineaG = this.fb.group({
      detallePedidoID: [data?.detallePedidoID ?? 0],
      productoId:      [data?.productoID ?? null, Validators.required],
      cantidad:        [data?.cantidad ?? 1, [Validators.required, Validators.min(1)]],
      precioUnitario:  [data?.precioUnitario ?? 0],
      subtotal:        [data?.importeLinea   ?? 0],
    });
    this.lineas.push(lineaG);
    this.recalcularSubtotalLinea(this.lineas.length - 1);
  }

  /**
   * Elimina una línea del FormArray (mínimo 1 línea obligatoria).
   * @param index Índice de la línea a eliminar
   */
  eliminarLinea(index: number): void {
    if (this.lineas.length <= 1) return;
    this.lineas.removeAt(index);
    this.recalcularTotales();
  }

  /**
   * Rellena el precio unitario automáticamente al seleccionar un producto.
   * @param index Índice de la línea modificada
   */
  onProductoChange(index: number): void {
    const row        = this.lineas.at(index);
    const productoId = row.get('productoId')?.value;
    const pp = this.productosDelProveedor.find((p: any) => p.productoID === productoId);
    if (pp) {
      row.get('precioUnitario')?.setValue(
        Number(pp.precioProveedor ?? pp.precio ?? pp.precioUnitario ?? 0),
        { emitEvent: false }
      );
    }
    this.recalcularSubtotalLinea(index);
  }

  /**
   * Recalcula el subtotal de la línea cuando cambia cantidad o precio.
   * @param index Índice de la línea modificada
   */
  onLineaChange(index: number): void {
    this.recalcularSubtotalLinea(index);
  }

  /**
   * Actualiza el campo `subtotal` de la línea y dispara el recálculo global.
   * @param index Índice de la línea
   * @internal
   */
  private recalcularSubtotalLinea(index: number): void {
    const row  = this.lineas.at(index);
    const cant = Number(row.get('cantidad')?.value       ?? 0);
    const prec = Number(row.get('precioUnitario')?.value ?? 0);
    const sub  = cant * prec;
    row.get('subtotal')?.setValue(
      Number.isFinite(sub) ? sub : 0,
      { emitEvent: false }
    );
    this.recalcularTotales();
  }

  /** Recalcula la base imponible y el total con IVA a partir de las líneas. */
  recalcularTotales(): void {
    this.baseImponible = this.lineas.controls.reduce((acc, ctrl) => {
      const v = Number(ctrl.get('subtotal')?.value ?? 0);
      return acc + (Number.isFinite(v) ? v : 0);
    }, 0);
    this.totalConIva = this.baseImponible * (1 + this.IVA);
  }

  /**
   * Valida y envía el formulario para crear o actualizar el pedido.
   * @remarks Si el formulario es inválido, marca todos los controles como tocados
   * para mostrar los errores de validación.
   */
  onGuardar(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    const raw = this.form.getRawValue();
    const detalles = (raw.lineas ?? []).map((l: any) => ({
      detallePedidoID: l.detallePedidoID ?? 0,
      productoID:      Number(l.productoId),
      cantidad:        Number(l.cantidad),
      precioUnitario:  Number(l.precioUnitario),
      descuento:       0,
    }));

    if (this.isEditMode) {
      const payload = {
        iva:           21,
        observaciones: raw.observaciones || null,
        detalles,
      };
      this.vm.actualizarPedido(this.pedidoId!, payload)
        .pipe(take(1)).subscribe({ next: () => this.onCancelar() });
    } else {
      const payload = {
        proveedorID:   Number(raw.proveedorId),
        fechaPedido:   new Date().toISOString(),
        iva:           21,
        observaciones: raw.observaciones || null,
        detalles,
      };
      this.vm.crearPedido(payload)
        .pipe(take(1)).subscribe({ next: () => this.onCancelar() });
    }
  }

  /** Vuelve al listado de pedidos sin guardar cambios. */
  onCancelar(): void {
    this.router.navigate(['/dashboard/pedidos']);
  }
}
