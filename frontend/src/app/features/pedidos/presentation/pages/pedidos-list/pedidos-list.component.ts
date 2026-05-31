import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, Inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, combineLatest, map, startWith, take } from 'rxjs';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import {
  MatDialog, MatDialogModule,
  MatDialogRef, MAT_DIALOG_DATA
} from '@angular/material/dialog';

import { PedidosViewModel } from '../../viewmodels/pedidos.viewmodels';
import type { PedidoDTO } from '../../../domain/dtos/PedidoDTO';

/**
 * Dialogo de confirmación antes de eliminar un pedido.
 * @internal
 */
@Component({
  selector: 'app-pedido-confirm-delete',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
  template: `
  <div class="cd-head">
    <div class="cd-icon">🗑️</div>
    <div class="cd-info"><div class="cd-title">Eliminar pedido</div><div class="cd-sub">{{ data.numeroPedido }}</div></div>
  </div>
  <div mat-dialog-content class="cd-body">
    <p>¿Estás seguro de que deseas eliminar el pedido <strong>{{ data.numeroPedido }}</strong>?</p>
    <p class="cd-warn">Esta acción no se puede deshacer.</p>
  </div>
  <div mat-dialog-actions align="end" class="cd-actions">
    <button mat-stroked-button (click)="ref.close(false)">Cancelar</button>
    <button mat-raised-button color="warn" (click)="ref.close(true)"><mat-icon>delete</mat-icon> Eliminar</button>
  </div>`,
  styles: [`.cd-head{display:flex;align-items:center;gap:14px;padding:20px 22px 16px; border-bottom:1px solid #fecaca;background:#fff5f5;} .cd-icon{width:44px;height:44px;border-radius:12px;background:rgba(239,68,68,.1); display:grid;place-items:center;font-size:20px;flex-shrink:0;} .cd-info{flex:1;}.cd-title{font-size:16px;font-weight:800;color:#991b1b;} .cd-sub{font-size:13px;color:#6b7280;margin-top:2px;} .cd-body{padding:16px 22px!important;} .cd-body p{font-size:14px;color:#374151;margin:0 0 8px;} .cd-warn{font-size:13px;color:#ef4444!important;font-weight:600;} .cd-actions{padding:12px 22px!important;gap:8px;}`]
})
export class PedidoConfirmDeleteDialogComponent {
  constructor(public readonly ref: MatDialogRef<PedidoConfirmDeleteDialogComponent>, @Inject(MAT_DIALOG_DATA) public readonly data: { numeroPedido: string }) {}
}

/**
 * Dialogo de confirmación antes de marcar un pedido como Enviado.
 * @internal
 */
@Component({
  selector: 'app-pedido-confirm-enviar',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
  template: `
  <div class="ce-head">
    <div class="ce-icon">📤</div>
    <div class="ce-info"><div class="ce-title">Marcar como Enviado</div><div class="ce-sub">{{ data.numeroPedido }}</div></div>
  </div>
  <div mat-dialog-content class="ce-body">
    <p>¿Confirmas que el pedido <strong>{{ data.numeroPedido }}</strong> ha sido enviado?</p>
    <p class="ce-note">El pedido pasará a estado <strong>Enviado</strong> y ya no podrá editarse.</p>
  </div>
  <div mat-dialog-actions align="end" class="ce-actions">
    <button mat-stroked-button (click)="ref.close(false)">Cancelar</button>
    <button mat-raised-button color="primary" (click)="ref.close(true)"><mat-icon>send</mat-icon> Confirmar envío</button>
  </div>`,
  styles: [`.ce-head{display:flex;align-items:center;gap:14px;padding:20px 22px 16px;border-bottom:1px solid #bfdbfe;background:#eff6ff;} .ce-icon{width:44px;height:44px;border-radius:12px;background:rgba(59,130,246,.12);display:grid;place-items:center;font-size:20px;flex-shrink:0;} .ce-info{flex:1;} .ce-title{font-size:16px;font-weight:800;color:#1e3a8a;} .ce-sub{font-size:13px;color:#6b7280;margin-top:2px;} .ce-body{padding:16px 22px!important;} .ce-body p{font-size:14px;color:#374151;margin:0 0 8px;} .ce-note{font-size:13px;color:#1d4ed8!important;font-weight:600;} .ce-actions{padding:12px 22px!important;gap:8px;}`]
})
export class PedidoConfirmEnviarDialogComponent {
  constructor(public readonly ref: MatDialogRef<PedidoConfirmEnviarDialogComponent>, @Inject(MAT_DIALOG_DATA) public readonly data: { numeroPedido: string }) {}
}

/**
 * Dialogo de solo lectura con los detalles completos de un pedido.
 * @internal
 */
@Component({
  selector: 'app-pedido-detalle-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule, MatDividerModule],
  template: `
  <div class="dh">
    <div class="dh-icon">📦</div>
    <div class="dh-info">
      <div class="dh-num">{{ data.numeroPedido }}</div>
      <div class="dh-prov">{{ data.proveedorNombre || (data.proveedor?.razonSocial) || 'Sin proveedor' }}</div>
    </div>
    <div class="dh-badge" [ngClass]="badgeClass">{{ data.estadoNombre || 'Sin estado' }}</div>
    <button mat-icon-button class="dh-close" (click)="ref.close()"><mat-icon>close</mat-icon></button>
  </div>
  <div class="kpis">
    <div class="kpi"><div class="kpi-label">Subtotal</div><div class="kpi-value">{{ (data.importeTotal ?? data.subtotal) | currency:'EUR' }}</div></div>
    <div class="kpi-sep"></div>
    <div class="kpi"><div class="kpi-label">Líneas</div><div class="kpi-value">{{ (data.detalles || []).length }}</div></div>
  </div>
  <div mat-dialog-content class="db">
    <div class="sec-label"><mat-icon class="sec-ico">calendar_today</mat-icon>Fecha</div>
    <div class="row-1"><div class="field"><span class="fk">Fecha de pedido</span><span class="fv">{{ fmt(data.fechaPedido) }}</span></div></div>
    <div class="sec-label"><mat-icon class="sec-ico">manage_accounts</mat-icon>Gestión</div>
    <div class="row-1"><div class="field"><span class="fk">Observaciones</span><span class="fv subdued">{{ data.observaciones || 'Sin observaciones' }}</span></div></div>
    <div class="sec-label"><mat-icon class="sec-ico">list_alt</mat-icon>Líneas de producto</div>
    <div class="lines-wrap" *ngIf="(data.detalles || []).length > 0">
      <div class="line-head"><span>Producto</span><span>Código</span><span class="center">Cant.</span><span class="right">P.Unit.</span><span class="center">Dto.</span><span class="right">Importe</span></div>
      <div class="line-row" *ngFor="let d of data.detalles">
        <span class="pn">{{ d.productoNombre }}</span><span class="cod">{{ d.codigoProducto }}</span><span class="center">{{ d.cantidad }}</span>
        <span class="right">{{ d.precioUnitario | currency:'EUR' }}</span><span class="center dto">{{ d.descuento }}%</span><span class="right money">{{ d.importeLinea | currency:'EUR' }}</span>
      </div>
    </div>
  </div>
  <div mat-dialog-actions align="end" class="da"><button mat-stroked-button (click)="ref.close()">Cerrar</button></div>`,
  styles: [`.dh{display:flex;align-items:center;gap:14px;padding:22px 22px 18px; background:linear-gradient(135deg,#1e1b4b 0%,#312e81 100%);border-radius:4px 4px 0 0;} .dh-icon{width:48px;height:48px;border-radius:14px;background:rgba(255,255,255,.12);display:grid;place-items:center;font-size:22px;flex-shrink:0;} .dh-info{flex:1;min-width:0;} .dh-num{font-size:20px;font-weight:900;color:#fff;letter-spacing:-.4px;line-height:1.2;white-space:nowrap;overflow:hidden;text-overflow:ellipsis;} .dh-prov{font-size:13px;color:rgba(255,255,255,.6);margin-top:2px;} .dh-badge{padding:5px 14px;border-radius:999px;flex-shrink:0;font-size:12px;font-weight:800;white-space:nowrap;background:rgba(255,255,255,.15);color:#fff;border:1px solid rgba(255,255,255,.25);} .dh-badge.pendiente{background:rgba(245,158,11,.25);color:#fde68a;border-color:rgba(245,158,11,.4);} .dh-badge.completado{background:rgba(16,185,129,.25);color:#a7f3d0;border-color:rgba(16,185,129,.4);} .dh-close{color:rgba(255,255,255,.7)!important;flex-shrink:0;} .kpis{display:flex;align-items:center;padding:14px 22px;background:#f8fafc;border-bottom:1px solid #e2e8f0;} .kpi{flex:1;text-align:center;padding:0 8px;}.kpi-sep{width:1px;height:32px;background:#e2e8f0;flex-shrink:0;} .kpi-label{font-size:10px;font-weight:700;color:#94a3b8;text-transform:uppercase;letter-spacing:.6px;} .kpi-value{font-size:15px;font-weight:900;color:#0f172a;margin-top:2px;} .db{padding:6px 22px 0;min-width:600px;} .sec-label{display:flex;align-items:center;gap:6px;font-size:11px;font-weight:800;text-transform:uppercase;letter-spacing:.8px;color:#64748b;margin:18px 0 10px;} .row-1{display:grid;grid-template-columns:1fr;gap:8px;} .field{background:#f8fafc;border-radius:10px;padding:10px 14px;border:1px solid #e2e8f0;} .fk{font-size:11px;font-weight:700;color:#94a3b8;display:block;margin-bottom:3px;} .fv{font-size:14px;font-weight:700;color:#0f172a;}.fv.subdued{font-weight:500;color:#64748b;font-size:13px;} .lines-wrap{border:1px solid #e2e8f0;border-radius:12px;overflow:hidden;} .line-head{display:grid;grid-template-columns:2.2fr 1.1fr .5fr .9fr .5fr .9fr;gap:8px;padding:9px 14px;background:#f1f5f9;font-size:10px;font-weight:800;color:#64748b;text-transform:uppercase;letter-spacing:.4px;} .line-row{display:grid;grid-template-columns:2.2fr 1.1fr .5fr .9fr .5fr .9fr;gap:8px;padding:10px 14px;border-top:1px solid #e2e8f0;font-size:13px;color:#374151;} .da{padding:12px 22px!important;border-top:1px solid #f1f5f9;}`]
})
export class PedidoDetalleDialogComponent {
  constructor(public readonly ref: MatDialogRef<PedidoDetalleDialogComponent>, @Inject(MAT_DIALOG_DATA) public readonly data: any) {}

  /**
   * Clase CSS del badge de estado basada en el nombre del estado del pedido.
   * @returns `'pendiente'` | `'completado'` | `''`
   */
  get badgeClass(): string {
    const e = (this.data.estadoNombre || '').toLowerCase();
    if (e.includes('pend')) return 'pendiente';
    if (e.includes('recib')) return 'completado';
    return '';
  }

  /**
   * Formatea una fecha ISO a formato español (DD/MM/AAAA).
   * @param d Cadena de fecha ISO, nula o indefinida
   * @returns Fecha formateada o `'—'` si el valor es nulo
   */
  fmt(d: string | null | undefined): string {
    if (!d) return '—';
    try { return new Date(d).toLocaleDateString('es-ES'); }
    catch { return String(d); }
  }
}

/**
 * Listado de pedidos con filtros, cambio de estado y eliminación.
 * @remarks Punto de entrada principal para gestión de pedidos.
 */
@Component({
  selector: 'app-pedidos-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatButtonModule, MatIconModule, MatChipsModule, MatProgressSpinnerModule, MatDialogModule, MatSelectModule, MatFormFieldModule],
  templateUrl: './pedidos-list.component.html',
  styleUrls: ['./pedidos-list.component.scss']
})
export class PedidosListComponent implements OnInit {
  private readonly router = inject(Router);
  private readonly dialog = inject(MatDialog);
  readonly vm             = inject(PedidosViewModel);

  filterProveedor = new FormControl<number | null>(null);
  filterEstado    = new FormControl<number | null>(null);
  isEmpty$!: Observable<boolean>;
  pedidosFiltrados$!: Observable<PedidoDTO[]>;
  proveedores$: any[] = [];

  /** Caché local de estados para consultas síncronas en {@link onEstadoChange}. */
  private estadosList: any[] = [];

  /** Carga pedidos, estados, recibidos y proveedores; construye los observables de filtro. */
  ngOnInit(): void {
    this.vm.cargarPedidos();
    this.vm.cargarEstados();
    this.vm.cargarRecibidos();
    this.vm.proveedores$.pipe(take(1)).subscribe(d => this.proveedores$ = d ?? []);
    this.vm.estados$.subscribe(e => this.estadosList = e ?? []);

    this.isEmpty$ = this.vm.pedidos$.pipe(map(p => (p ?? []).length === 0));
    this.pedidosFiltrados$ = combineLatest([
      this.vm.pedidos$,
      this.filterProveedor.valueChanges.pipe(startWith(null)),
      this.filterEstado.valueChanges.pipe(startWith(null)),
    ]).pipe(
      map(([pedidos, provId, estado]) => {
        let list = pedidos ?? [];
        if (provId)  list = list.filter(p => (p as any).proveedorID === provId);
        if (estado)  list = list.filter(p => p.estadoID === estado);
        return list;
      })
    );
  }

  /**
   * Indica si el pedido está en estado Pendiente (puede editarse).
   * @param pedido Pedido a evaluar
   */
  esPendiente(pedido: PedidoDTO): boolean {
    return (pedido.estadoNombre || '').toLowerCase().includes('pend');
  }

  /**
   * Indica si el pedido puede eliminarse (solo Recibido o Cancelado).
   * @param pedido Pedido a evaluar
   */
  puedeEliminar(pedido: PedidoDTO): boolean {
    const e = (pedido.estadoNombre || '').toLowerCase();
    return e.includes('recib') || e.includes('cancel');
  }

  /**
   * Determina si una opción de estado debe estar deshabilitada en el selector.
   * @param pedido Pedido cuyo estado actual se compara
   * @param opcionEstado Estado candidato
   */
  isOptionDisabled(pedido: PedidoDTO, opcionEstado: any): boolean {
    const actual  = (pedido.estadoNombre || '').toLowerCase();
    const destino = (opcionEstado.nombreEstado || '').toLowerCase();

    if (pedido.estadoID === opcionEstado.estadoID) return false;
    if (actual.includes('recib')) return true;

    if (destino.includes('cancel') || destino.includes('envi')) {
      return !actual.includes('pend');
    }
    if (destino.includes('recib')) {
      return !actual.includes('envi');
    }
    return true;
  }

  /**
   * Clase CSS del chip de estado según el nombre del estado.
   * @param estadoNombre Nombre del estado del pedido
   */
  chipClass(estadoNombre: string | undefined): string {
    const e = (estadoNombre ?? '').toLowerCase();
    if (e.includes('pend'))  return 'chip-pendiente';
    if (e.includes('recib')) return 'chip-completado';
    return 'chip-default';
  }

  /**
   * Formatea una fecha ISO a formato español (DD/MM/AAAA).
   * @param d Cadena de fecha ISO o nula
   */
  formatDate(d: string | null | undefined): string {
    if (!d) return '—';
    try { return new Date(d).toLocaleDateString('es-ES'); } catch { return String(d); }
  }

  /**
   * Gestiona el cambio de estado de un pedido.
   * Muestra confirmación cuando el destino es "Enviado".
   * @param pedido Pedido al que se cambia el estado
   * @param nuevoId ID del nuevo estado seleccionado
   */
  onEstadoChange(pedido: PedidoDTO, nuevoId: number): void {
    const estadoDestino = this.estadosList.find(e => e.estadoID === nuevoId);
    const nombreDestino = (estadoDestino?.nombreEstado || '').toLowerCase();

    if (nombreDestino.includes('envi') && this.esPendiente(pedido)) {
      const ref = this.dialog.open(PedidoConfirmEnviarDialogComponent, {
        width: '440px',
        data: { numeroPedido: pedido.numeroPedido }
      });
      ref.afterClosed().pipe(take(1)).subscribe(confirmado => {
        if (confirmado) {
          this.vm.cambiarEstado(pedido, nuevoId).pipe(take(1)).subscribe();
        } else {
          this.vm.cargarPedidos();
        }
      });
      return;
    }

    this.vm.cambiarEstado(pedido, nuevoId).pipe(take(1)).subscribe();
  }

  /** Limpia los filtros activos de proveedor y estado. */
  clearFilters(): void {
    this.filterProveedor.setValue(null);
    this.filterEstado.setValue(null);
  }

  /** Navega al formulario de creación de nuevo pedido. */
  onCrear(): void { this.router.navigate(['/dashboard/pedidos/nuevo']); }

  /**
   * Navega al formulario de edición si el pedido está en Pendiente.
   * @param p Pedido a editar
   */
  onEditar(p: PedidoDTO): void {
    if (this.esPendiente(p)) this.router.navigate(['/dashboard/pedidos/editar', (p as any).pedidoID]);
  }

  /**
   * Abre el dialogo de detalles completos del pedido.
   * @param p Pedido del que mostrar detalles
   */
  onVerDetalles(p: PedidoDTO): void {
    this.vm.obtenerPedidoPorId((p as any).pedidoID).pipe(take(1)).subscribe(d => {
      this.dialog.open(PedidoDetalleDialogComponent, { width: '740px', data: d });
    });
  }

  /**
   * Muestra confirmación y elimina el pedido si es Recibido o Cancelado.
   * @param p Pedido a eliminar
   */
  onEliminar(p: PedidoDTO): void {
    if (!this.puedeEliminar(p)) {
      this.vm.notify.showError('Solo se pueden eliminar pedidos recibidos o cancelados');
      return;
    }
    const ref = this.dialog.open(PedidoConfirmDeleteDialogComponent, {
      width: '420px',
      data: { numeroPedido: p.numeroPedido }
    });
    ref.afterClosed().pipe(take(1)).subscribe(conf => {
      if (conf) this.vm.eliminarPedido((p as any).pedidoID).pipe(take(1)).subscribe();
    });
  }
}