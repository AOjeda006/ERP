import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, combineLatest } from 'rxjs';
import { map, startWith, take } from 'rxjs/operators';
import { Container, TYPES } from '../../../../../core/container/container';
import { ProveedoresViewModel, ProveedorDTO, ProductoProveedorDTO } from '../../viewmodels/proveedores.viewmodel';

/** @internal Datos inyectados al diálogo de detalle: el proveedor a mostrar. */
type ProveedorDetalleData = { proveedor: ProveedorDTO };

/**
 * Dialogo de detalles del proveedor con sus productos suministrados.
 * @internal
 */
@Component({
  selector: 'app-proveedor-detalle-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule],
  template: `
    <div class="dh">
      <div class="dh-icon">🏢</div>
      <div class="dh-info">
        <div class="dh-name">{{ data.proveedor.razonSocial }}</div>
        <div class="dh-sub" *ngIf="data.proveedor.nombreComercial">{{ data.proveedor.nombreComercial }}</div>
      </div>
      <button mat-icon-button class="dh-close" (click)="ref.close()"><mat-icon>close</mat-icon></button>
    </div>

    <div mat-dialog-content class="db">
      <div class="sec-label"><mat-icon class="sec-ico">info</mat-icon>Información del proveedor</div>
      <div class="field-row">
        <div class="field"><span class="fk">CIF</span><span class="fv">{{ data.proveedor.cif || '—' }}</span></div>
        <div class="field"><span class="fk">Nombre comercial</span><span class="fv">{{ data.proveedor.nombreComercial || '—' }}</span></div>
        <div class="field"><span class="fk">Teléfono</span><span class="fv">{{ data.proveedor.telefono || '—' }}</span></div>
        <div class="field"><span class="fk">Email</span><span class="fv">{{ data.proveedor.email || '—' }}</span></div>
        <div class="field"><span class="fk">Persona de contacto</span><span class="fv">{{ data.proveedor.personaContacto || '—' }}</span></div>
      </div>

      <div class="sec-label"><mat-icon class="sec-ico">inventory_2</mat-icon>Productos suministrados</div>
      <div class="loading-pp" *ngIf="loadingPP"><mat-spinner diameter="20"></mat-spinner><span>Cargando...</span></div>
      <div class="no-lines" *ngIf="!loadingPP && productos.length === 0">Sin productos registrados</div>
      <div class="pp-wrap" *ngIf="!loadingPP && productos.length > 0">
        <div class="pp-head"><span>Producto</span><span>Código</span><span class="right">Precio</span><span class="center">Plazo</span></div>
        <div class="pp-row" *ngFor="let pp of productos">
          <span class="pp-name">{{ pp.productoNombre }}</span>
          <span class="pp-code">{{ pp.codigoProducto || '—' }}</span>
          <span class="right money">{{ pp.precioProveedor | currency:'EUR' }}</span>
          <span class="center">{{ pp.tiempoEntregaDias ?? '—' }}</span>
        </div>
      </div>
    </div>

    <div mat-dialog-actions align="end" class="da">
      <button mat-stroked-button (click)="ref.close()">Cerrar</button>
    </div>
  `,
  styles: [`.dh{display:flex;align-items:center;gap:14px;padding:22px 22px 18px;background:linear-gradient(135deg,#1e3a5f 0%,#1e40af 100%);border-radius:4px 4px 0 0;}
    .dh-icon{width:48px;height:48px;border-radius:14px;background:rgba(255,255,255,.15);display:grid;place-items:center;font-size:22px;flex-shrink:0;}
    .dh-info{flex:1;min-width:0;}.dh-name{font-size:18px;font-weight:900;color:#fff;}
    .dh-sub{font-size:12px;color:rgba(255,255,255,.6);margin-top:2px;}.dh-close{color:rgba(255,255,255,.7)!important;}
    .db{padding:6px 22px 0;min-width:580px;}
    .sec-label{display:flex;align-items:center;gap:6px;font-size:11px;font-weight:800;text-transform:uppercase;letter-spacing:.8px;color:#64748b;margin:18px 0 10px;}
    .sec-ico{font-size:14px!important;width:14px!important;height:14px!important;color:#6366f1;}
    .field-row{display:grid;grid-template-columns:1fr 1fr;gap:8px;}
    .field{background:#f8fafc;border-radius:10px;padding:10px 14px;border:1px solid #e2e8f0;}
    .fk{font-size:11px;font-weight:700;color:#94a3b8;display:block;margin-bottom:3px;}.fv{font-size:14px;font-weight:700;color:#0f172a;}
    .loading-pp{display:flex;align-items:center;gap:8px;color:#6b7280;padding:10px 0;font-size:13px;}
    .no-lines{text-align:center;padding:16px;color:#94a3b8;font-size:13px;border:1px dashed #e2e8f0;border-radius:10px;}
    .pp-wrap{border:1px solid #e2e8f0;border-radius:10px;overflow:hidden;}
    .pp-head{display:grid;grid-template-columns:2fr 1.2fr .9fr .8fr;gap:8px;padding:8px 14px;background:#f1f5f9;font-size:10px;font-weight:800;color:#64748b;text-transform:uppercase;}
    .pp-row{display:grid;grid-template-columns:2fr 1.2fr .9fr .8fr;gap:8px;padding:9px 14px;border-top:1px solid #e2e8f0;font-size:13px;}
    .pp-name{font-weight:700;color:#0f172a;}.pp-code{color:#6b7280;font-size:12px;}
    .right{text-align:right;}.center{text-align:center;}.money{font-weight:700;color:#6366f1;}
    .da{padding:12px 22px!important;border-top:1px solid #f1f5f9;}`]
})
export class ProveedorDetalleDialogComponent implements OnInit {
  loadingPP = true;
  productos: ProductoProveedorDTO[] = [];
  private readonly vm = Container.getInstance().resolve<ProveedoresViewModel>(TYPES.ProveedoresListViewModel);

  constructor(
    public readonly ref: MatDialogRef<ProveedorDetalleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ProveedorDetalleData
  ) {}

  /** Carga los productos suministrados por el proveedor inyectado vía datos del diálogo. */
  ngOnInit(): void {
    const id = this.data?.proveedor?.proveedorID;
    if (!id) { this.loadingPP = false; return; }
    this.vm.obtenerProductosDeProveedor(id).pipe(take(1)).subscribe(res => {
      this.productos = res;
      this.loadingPP = false;
    });
  }
}

/**
 * Pantalla de listado de proveedores con búsqueda por nombre/CIF/email.
 * @remarks Read-only: no expone acciones de creación/edición/eliminación.
 */
@Component({
  selector: 'app-proveedores',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatCardModule, MatTableModule, MatFormFieldModule, MatInputModule,
    MatIconModule, MatButtonModule, MatDialogModule, MatProgressSpinnerModule,
    MatChipsModule
  ],
  templateUrl: './proveedores.component.html',
  styleUrl: './proveedores.component.scss'
})
export class ProveedoresComponent implements OnInit {
  vm!: ProveedoresViewModel;
  search = new FormControl('', { nonNullable: true });
  displayedColumns = ['razon', 'contacto', 'acciones'];
  data$!: Observable<ProveedorDTO[]>;

  constructor(private dialog: MatDialog) {}

  /** Resuelve el ViewModel del contenedor DI, carga los datos y construye el observable de filtro. */
  ngOnInit(): void {
    this.vm = Container.getInstance().resolve<ProveedoresViewModel>(TYPES.ProveedoresListViewModel);
    this.vm.cargarTodos();
    this.data$ = combineLatest([
      this.vm.proveedores$,
      this.search.valueChanges.pipe(startWith(''))
    ]).pipe(
      map(([items, q]) => {
        const query = q.toLowerCase().trim();
        if (!query) return items;
        return items.filter(p =>
          p.razonSocial.toLowerCase().includes(query) ||
          p.cif.toLowerCase().includes(query) ||
          (p.email ?? '').toLowerCase().includes(query)
        );
      })
    );
  }

  /** Recarga el listado de proveedores desde el backend. */
  refresh(): void { this.vm.cargarTodos(); }

  /**
   * Abre el dialogo de detalles del proveedor.
   * @param p Proveedor a mostrar
   */
  onDetalles(p: ProveedorDTO): void {
    this.dialog.open(ProveedorDetalleDialogComponent, {
      width: '680px', maxHeight: '90vh', data: { proveedor: p }
    });
  }
}