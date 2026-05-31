import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable, of } from 'rxjs';
import { catchError, finalize, map, startWith, switchMap } from 'rxjs/operators';


import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import {
  MatDialog, MatDialogModule,
  MatDialogRef, MAT_DIALOG_DATA
} from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';

import { environment } from '../../../../../../environments/environment';

/** @internal Línea de detalle de un pedido recibido. */
interface DetallePedidoDTO {
  detallePedidoID: number;
  productoID: number;
  productoNombre: string;
  codigoProducto: string;
  cantidad: number;
  precioUnitario: number;
  descuento: number;
  importeLinea: number;
  activo: boolean;
}

/** @internal Pedido recibido con su proveedor, estado y líneas de detalle. */
interface PedidoDetalleDTO {
  pedidoID: number;
  numeroPedido: string;
  proveedor: { razonSocial: string };
  estado: { nombreEstado: string };
  fechaPedido: string;
  fechaRecepcion: string | null;
  observaciones: string | null;
  detalles: DetallePedidoDTO[];
}

/** @internal Producto del catálogo tal como lo devuelve el backend, usado para enriquecer el stock. */
interface ProductoBackendDTO {
  productoID: number;
  codigoProducto: string;
  nombreProducto: string;
  descripcion: string | null;
  unidadMedida: string;
  precioUnitario: number;
  categoriaNombre: string;
}

/** @internal Categoría de producto (identificador y nombre). */
interface CategoriaDTO {
  categoriaID: number;
  nombreCategoria: string;
}

/**
 * Producto agregado para la vista de almacén.
 *
 * @remarks
 * Combina los datos del catálogo con el **stock acumulado** a partir de las líneas
 * de todos los pedidos en estado Recibido, junto al número de pedidos que lo contienen.
 */
export interface ProductoStockItem {
  productoID: number;
  codigoProducto: string;
  nombreProducto: string;
  descripcion: string | null;
  unidadMedida: string;
  precioUnitario: number;
  categoriaNombre: string;
  stockRecibido: number;
  pedidosCount: number;
}

/**
 * Dialogo de detalle de stock de un producto en el almacén.
 * @internal
 */
@Component({
  selector: 'app-producto-stock-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule, MatDividerModule],
  template: `
  <div class="dh">
    <div class="dh-icon">🏭</div>
    <div class="dh-info">
      <div class="dh-name">{{ data.item.nombreProducto }}</div>
      <div class="dh-code">{{ data.item.codigoProducto }}</div>
    </div>
    <div class="dh-stock">{{ data.item.stockRecibido }} {{ data.item.unidadMedida }}</div>
    <button mat-icon-button class="dh-close" (click)="ref.close()">
      <mat-icon>close</mat-icon>
    </button>
  </div>

  <div class="kpis">
    <div class="kpi"><div class="kpi-label">Categoría</div><div class="kpi-value">{{ data.item.categoriaNombre || '—' }}</div></div>
    <div class="kpi-sep"></div>
    <div class="kpi"><div class="kpi-label">Precio unit.</div><div class="kpi-value">{{ data.item.precioUnitario | currency:'EUR' }}</div></div>
    <div class="kpi-sep"></div>
    <div class="kpi"><div class="kpi-label">Stock recibido</div><div class="kpi-value accent">{{ data.item.stockRecibido }} {{ data.item.unidadMedida }}</div></div>
    <div class="kpi-sep"></div>
    <div class="kpi"><div class="kpi-label">Pedidos</div><div class="kpi-value">{{ data.item.pedidosCount }}</div></div>
  </div>

  <div mat-dialog-content class="db">
    <div class="sec-label">
      <mat-icon class="sec-ico">info</mat-icon>Descripción
    </div>
    <p class="desc-text">{{ data.item.descripcion || 'Sin descripción disponible.' }}</p>

    <div class="sec-label" *ngIf="data.pedidos.length > 0">
      <mat-icon class="sec-ico">receipt_long</mat-icon>Pedidos recibidos que contienen este producto
    </div>
    <div class="pedidos-list" *ngIf="data.pedidos.length > 0">
      <div class="ped-row" *ngFor="let p of data.pedidos">
        <span class="ped-num">{{ p.numeroPedido }}</span>
        <span class="ped-prov">{{ p.proveedor.razonSocial }}</span>
        <span class="ped-fecha">{{ fmt(p.fechaRecepcion || p.fechaPedido) }}</span>
        <span class="ped-cant">{{ getCantidadEnPedido(p) }} {{ data.item.unidadMedida }}</span>
      </div>
    </div>
  </div>

  <div mat-dialog-actions align="end" class="da">
    <button mat-stroked-button (click)="ref.close()">Cerrar</button>
  </div>
  `,
  styles: [`
    .dh {
      display:flex; align-items:center; gap:14px;
      padding:22px 22px 18px;
      background:linear-gradient(135deg,#064e3b 0%,#065f46 100%);
      border-radius:4px 4px 0 0;
    }
    .dh-icon {
      width:48px; height:48px; border-radius:14px;
      background:rgba(255,255,255,.15); display:grid;
      place-items:center; font-size:22px; flex-shrink:0;
    }
    .dh-info { flex:1; min-width:0; }
    .dh-name { font-size:18px; font-weight:900; color:#fff; }
    .dh-code { font-size:12px; color:rgba(255,255,255,.6); margin-top:2px; }
    .dh-stock {
      padding:6px 14px; border-radius:999px; flex-shrink:0;
      font-size:13px; font-weight:800; color:#a7f3d0;
      background:rgba(255,255,255,.15); border:1px solid rgba(255,255,255,.25);
    }
    .dh-close { color:rgba(255,255,255,.7)!important; }
    .kpis {
      display:flex; align-items:center; padding:14px 22px;
      background:#f8fafc; border-bottom:1px solid #e2e8f0;
    }
    .kpi { flex:1; text-align:center; padding:0 8px; }
    .kpi-sep { width:1px; height:32px; background:#e2e8f0; flex-shrink:0; }
    .kpi-label { font-size:10px; font-weight:700; color:#94a3b8; text-transform:uppercase; letter-spacing:.6px; }
    .kpi-value { font-size:14px; font-weight:900; color:#0f172a; margin-top:2px; }
    .kpi-value.accent { color:#10b981; }
    .db { padding:6px 22px 0; min-width:580px; }
    .sec-label {
      display:flex; align-items:center; gap:6px;
      font-size:11px; font-weight:800; text-transform:uppercase;
      letter-spacing:.8px; color:#64748b; margin:18px 0 10px;
    }
    .sec-ico { font-size:14px!important; width:14px!important; height:14px!important; color:#10b981; }
    .desc-text { font-size:14px; color:#374151; line-height:1.6; margin:0 0 8px; }
    .pedidos-list { border:1px solid #e2e8f0; border-radius:10px; overflow:hidden; }
    .ped-row {
      display:grid; grid-template-columns:1.5fr 1.5fr 1fr .8fr;
      gap:8px; padding:10px 14px; border-top:1px solid #e2e8f0; font-size:13px;
    }
    .ped-row:first-child { border-top:none; }
    .ped-num { font-weight:700; color:#111827; }
    .ped-prov { color:#6b7280; }
    .ped-fecha { color:#6b7280; }
    .ped-cant { font-weight:700; color:#10b981; text-align:right; }
    .da { padding:12px 22px!important; border-top:1px solid #f1f5f9; }
  `]
})
export class ProductoStockDialogComponent {
  constructor(
    public readonly ref: MatDialogRef<ProductoStockDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public readonly data: {
      item: ProductoStockItem;
      pedidos: PedidoDetalleDTO[];
    }
  ) { }

  /**
   * Suma la cantidad del producto actual en las líneas de un pedido concreto.
   * @param pedido Pedido del que calcular la cantidad
   * @returns Total de unidades del producto en ese pedido
   */
  getCantidadEnPedido(pedido: PedidoDetalleDTO): number {
    return pedido.detalles
      .filter(d => d.productoID === this.data.item.productoID)
      .reduce((acc, d) => acc + d.cantidad, 0);
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
 * Pantalla de inventario de almacén.
 * @remarks Agrega el stock de todos los pedidos en estado Recibido
 * y lo enriquece con datos del catálogo de productos.
 */
@Component({
  selector: 'app-almacen',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatCardModule, MatButtonModule, MatIconModule,
    MatFormFieldModule, MatSelectModule,
    MatProgressSpinnerModule, MatChipsModule, MatDialogModule
  ],
  templateUrl: './almacen.component.html',
  styleUrls: ['./almacen.component.scss']
})
export class AlmacenComponent implements OnInit {
  private readonly http = inject(HttpClient);
  private readonly dialog = inject(MatDialog);

  isLoading = false;

  filterCategoria = new FormControl<string | null>(null);

  allItems: ProductoStockItem[] = [];
  categorias: string[] = [];
  pedidosRecibidos: PedidoDetalleDTO[] = [];

  items$!: Observable<ProductoStockItem[]>;

  /** Inicia la carga del inventario del almacén al montar el componente. */
  ngOnInit(): void {
    this.cargar();
  }

  /**
   * Obtiene los pedidos en estado Recibido y agrega el stock de sus líneas de detalle.
   * Enriquece cada entrada con datos del catálogo de productos mediante {@link forkJoin}.
   */
  cargar(): void {
  this.isLoading = true;

  this.http.get<PedidoDetalleDTO[]>(`${environment.apiUrl}/Pedidos/recibidos`).pipe(
    catchError(() => of([])),
    switchMap(recibidos => {
      this.pedidosRecibidos = recibidos;

      const stockMap = new Map<number, ProductoStockItem>();

      for (const pedido of recibidos) {
        for (const detalle of (pedido.detalles ?? [])) {
          if (stockMap.has(detalle.productoID)) {
            const existing = stockMap.get(detalle.productoID)!;
            existing.stockRecibido += detalle.cantidad;
            existing.pedidosCount  += 1;
          } else {
            stockMap.set(detalle.productoID, {
              productoID:      detalle.productoID,
              codigoProducto:  detalle.codigoProducto,
              nombreProducto:  detalle.productoNombre,
              descripcion:     null,
              unidadMedida:    '',
              precioUnitario:  detalle.precioUnitario,
              categoriaNombre: '',
              stockRecibido:   detalle.cantidad,
              pedidosCount:    1,
            });
          }
        }
      }

      const items = [...stockMap.values()];

      if (items.length === 0) return of(items);

      const requests = items.map(item =>
        this.http.get<ProductoBackendDTO>(`${environment.apiUrl}/Productos/${item.productoID}`).pipe(
          catchError(() => of(null))
        )
      );

      return forkJoin(requests).pipe(
        map(detallados => {
          detallados.forEach((prod, i) => {
            if (!prod) return;
            items[i].categoriaNombre = prod.categoriaNombre ?? '';
            items[i].unidadMedida    = prod.unidadMedida    ?? '';
            items[i].descripcion     = prod.descripcion     ?? null;
          });
          return items;
        })
      );
    }),
    finalize(() => this.isLoading = false)
  ).subscribe(items => {
    this.allItems = items;

    this.categorias = [...new Set(items.map(i => i.categoriaNombre).filter(Boolean))].sort();

    this.items$ = this.filterCategoria.valueChanges.pipe(
      startWith(this.filterCategoria.value),
      map(cat => cat ? this.allItems.filter(i => i.categoriaNombre === cat) : this.allItems)
    );
  });
}

  /** Limpia el filtro de categoría activo. */
  clearFilter(): void {
    this.filterCategoria.setValue(null);
  }

  /**
   * Abre el dialogo de detalles de stock del producto seleccionado.
   * @param item Producto con datos de stock a mostrar
   */
  onDetalles(item: ProductoStockItem): void {
    const pedidosConProducto = this.pedidosRecibidos.filter(p =>
      (p.detalles ?? []).some(d => d.productoID === item.productoID)
    );
    this.dialog.open(ProductoStockDialogComponent, {
      width: '680px',
      maxHeight: '90vh',
      data: { item, pedidos: pedidosConProducto }
    });
  }

  /**
   * Devuelve la clase CSS de color según el nivel de stock.
   * @param stock Cantidad de unidades disponibles
   * @returns `stock-zero` | `stock-low` | `stock-ok`
   */
  stockClass(stock: number): string {
    if (stock === 0) return 'stock-zero';
    if (stock < 10)  return 'stock-low';
    return 'stock-ok';
  }
}
