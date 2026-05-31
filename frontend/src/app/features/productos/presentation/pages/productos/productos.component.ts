import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ProductoViewModel, ProductoDTO } from '../../viewmodels/producto.viewmodel';

/**
 * Dialogo de solo lectura con la información completa de un producto.
 * @internal
 */
@Component({
  selector: 'app-producto-detalle-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule, MatIconModule],
  template: `
    <div class="dh">
      <div class="dh-icon">📦</div>
      <div class="dh-info">
        <div class="dh-name">{{ data.producto.nombreProducto }}</div>
        <div class="dh-code">{{ data.producto.codigoProducto }}</div>
      </div>
      <button mat-icon-button class="dh-close" (click)="ref.close()"><mat-icon>close</mat-icon></button>
    </div>

    <div mat-dialog-content class="db">
      <div class="sec-label"><mat-icon class="sec-ico">info</mat-icon>Información del producto</div>
      <div class="field-row">
        <div class="field"><span class="fk">Categoría</span><span class="fv">{{ data.producto.categoriaNombre || '—' }}</span></div>
        <div class="field"><span class="fk">Unidad de medida</span><span class="fv">{{ data.producto.unidadMedida || '—' }}</span></div>
        <div class="field"><span class="fk">Precio unitario</span><span class="fv">{{ (data.producto.precioUnitario ?? 0) | currency:'EUR' }}</span></div>
      </div>
      <div class="sec-label" *ngIf="data.producto.descripcion"><mat-icon class="sec-ico">description</mat-icon>Descripción</div>
      <p class="desc-text" *ngIf="data.producto.descripcion">{{ data.producto.descripcion }}</p>
    </div>

    <div mat-dialog-actions align="end" class="da">
      <button mat-stroked-button (click)="ref.close()">Cerrar</button>
    </div>
  `,
  styles: [`.dh{display:flex;align-items:center;gap:14px;padding:22px 22px 18px;background:linear-gradient(135deg,#312e81 0%,#6366f1 100%);border-radius:4px 4px 0 0;}
    .dh-icon{width:48px;height:48px;border-radius:14px;background:rgba(255,255,255,.15);display:grid;place-items:center;font-size:22px;flex-shrink:0;}
    .dh-info{flex:1;min-width:0;}.dh-name{font-size:18px;font-weight:900;color:#fff;}
    .dh-code{font-size:12px;color:rgba(255,255,255,.6);margin-top:2px;}.dh-close{color:rgba(255,255,255,.7)!important;}
    .db{padding:6px 22px 0;min-width:480px;}
    .sec-label{display:flex;align-items:center;gap:6px;font-size:11px;font-weight:800;text-transform:uppercase;letter-spacing:.8px;color:#64748b;margin:18px 0 10px;}
    .sec-ico{font-size:14px!important;width:14px!important;height:14px!important;color:#6366f1;}
    .field-row{display:grid;grid-template-columns:1fr 1fr;gap:8px;}
    .field{background:#f8fafc;border-radius:10px;padding:10px 14px;border:1px solid #e2e8f0;}
    .fk{font-size:11px;font-weight:700;color:#94a3b8;display:block;margin-bottom:3px;}.fv{font-size:14px;font-weight:700;color:#0f172a;}
    .desc-text{font-size:14px;color:#374151;line-height:1.6;margin:0 0 8px;}
    .da{padding:12px 22px!important;border-top:1px solid #f1f5f9;}`]
})
export class ProductoDetalleDialogComponent {
  constructor(
    public readonly ref: MatDialogRef<ProductoDetalleDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { producto: ProductoDTO }
  ) {}
}

/**
 * Pantalla de catálogo de productos con filtro por nombre y categoría.
 * @remarks Read-only: no expone acciones de creación/edición/eliminación.
 */
@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatCardModule, MatTableModule, MatFormFieldModule,
    MatInputModule, MatIconModule, MatButtonModule,
    MatSelectModule, MatProgressSpinnerModule, MatDialogModule
  ],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.scss'
})
export class ProductosComponent implements OnInit {
  private readonly dialog = inject(MatDialog);

  vm!: ProductoViewModel;

  search          = new FormControl<string>('', { nonNullable: true });
  filterCategoria = new FormControl<string | null>(null);

  displayedColumns = ['nombre', 'categoria', 'precio', 'acciones'];

  categorias: string[] = [];

  productosFiltrados$!: Observable<ProductoDTO[]>;

  constructor(private productoVm: ProductoViewModel) {}

  /** Carga el catálogo de productos y construye el observable de filtro combinado. */
  ngOnInit(): void {
    this.vm = this.productoVm;
    this.vm.cargarTodos();

    this.productosFiltrados$ = combineLatest([
      this.vm.productos$,
      this.search.valueChanges.pipe(startWith('')),
      this.filterCategoria.valueChanges.pipe(startWith(null)),
    ]).pipe(
      map(([items, q, cat]) => {
        this.categorias = [...new Set(items.map(i => i.categoriaNombre).filter(Boolean))].sort();
        let result = items;
        if (q) {
          const query = q.toLowerCase();
          result = result.filter(p =>
            p.nombreProducto.toLowerCase().includes(query) ||
            p.codigoProducto.toLowerCase().includes(query)
          );
        }
        if (cat) result = result.filter(p => p.categoriaNombre === cat);
        return result;
      })
    );
  }

  /** Recarga el listado de productos desde el backend. */
  refresh(): void { this.vm.cargarTodos(); }

  /** Limpia los filtros de búsqueda activos. */
  clearFiltros(): void {
    this.search.setValue('');
    this.filterCategoria.setValue(null);
  }

  /**
   * Abre el dialogo con los detalles del producto seleccionado.
   * @param p Producto a mostrar
   */
  onDetalles(p: ProductoDTO): void {
    this.dialog.open(ProductoDetalleDialogComponent, {
      width: '560px', maxHeight: '90vh', data: { producto: p }
    });
  }
}