import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { NotificationService } from '../../../../core/services/notification.service';

/**
 * Representa un producto del catálogo tal como lo devuelve el backend.
 */
export interface ProductoDTO {
  productoID: number;
  codigoProducto: string;
  nombreProducto: string;
  descripcion?: string | null;
  unidadMedida: string;
  precioUnitario?: number | null;
  stockActual: number;
  categoriaNombre: string;
}

/**
 * ViewModel de solo lectura para el catálogo de productos.
 * @remarks La pantalla de Productos es read-only; no expone operaciones de escritura.
 * @see {@link ProductosComponent}
 */
@Injectable({ providedIn: 'root' })
export class ProductoViewModel {
  private readonly http         = inject(HttpClient);
  private readonly notification = inject(NotificationService);

  private readonly baseUrl = `${environment.apiUrl}/Productos`;

  private readonly _isLoading = new BehaviorSubject<boolean>(false);
  readonly isLoading$: Observable<boolean> = this._isLoading.asObservable();

  private readonly _productos = new BehaviorSubject<ProductoDTO[]>([]);
  readonly productos$: Observable<ProductoDTO[]> = this._productos.asObservable();

  /**
   * Carga todos los productos desde el backend y normaliza los campos numéricos.
   * @remarks Actualiza {@link productos$} e {@link isLoading$}.
   */
  cargarTodos(): void {
    this._isLoading.next(true);
    this.http.get<ProductoDTO[]>(this.baseUrl).pipe(
      tap((data) => {
        const normalized = (data ?? []).map(p => ({
          ...p,
          precioUnitario: Number(p.precioUnitario ?? 0),
          stockActual:    Number(p.stockActual    ?? 0),
        }));
        this._productos.next(normalized);
      }),
      catchError(() => {
        this.notification.showError('Error cargando productos');
        this._productos.next([]);
        return of([]);
      }),
      finalize(() => this._isLoading.next(false))
    ).subscribe();
  }
}
