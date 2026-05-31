import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { environment } from '../../../../../environments/environment';
import { NotificationService } from '../../../../core/services/notification.service';

/**
 * Proveedor tal como lo devuelve el backend.
 */
export interface ProveedorDTO {
  proveedorID: number;
  cif: string;
  razonSocial: string;
  nombreComercial: string | null;
  direccion: string | null;
  codigoPostal: string | null;
  ciudad: string | null;
  provincia: string | null;
  pais: string;
  telefono: string | null;
  email: string | null;
  personaContacto: string | null;
  activo: boolean;
  fechaAlta: string;
  fechaModificacion: string | null;
}

/**
 * Relación producto-proveedor devuelta por `/api/ProductosProveedores`.
 * @remarks `tiempoEntregaDias` es el campo correcto del backend; `plazoEntrega` es alias.
 */
export interface ProductoProveedorDTO {
  productoID: number;
  proveedorID: number;
  productoNombre: string;
  codigoProducto: string;
  razonSocial?: string;
  precioProveedor?: number | null;
  plazoEntrega?: number | null;
  tiempoEntregaDias?: number | null;
  activo?: boolean;
}

/**
 * ViewModel de solo lectura para el listado de proveedores.
 * @remarks La pantalla de Proveedores es read-only; no expone operaciones de escritura.
 * @see {@link ProveedoresComponent}
 */
@Injectable({ providedIn: 'root' })
export class ProveedoresViewModel {
  private readonly http         = inject(HttpClient);
  private readonly notification = inject(NotificationService);

  private readonly baseUrl = `${environment.apiUrl}/Proveedores`;
  private readonly ppUrl   = `${environment.apiUrl}/ProductosProveedores`;

  private readonly _isLoading = new BehaviorSubject<boolean>(false);
  readonly isLoading$ = this._isLoading.asObservable();

  private readonly _proveedores = new BehaviorSubject<ProveedorDTO[]>([]);
  readonly proveedores$ = this._proveedores.asObservable();

  /**
   * Carga todos los proveedores activos desde el backend.
   * @remarks Actualiza {@link proveedores$} e {@link isLoading$}.
   */
  cargarTodos(): void {
    this._isLoading.next(true);
    this.http.get<ProveedorDTO[]>(this.baseUrl).pipe(
      tap(data => this._proveedores.next(data ?? [])),
      catchError(() => {
        this.notification.showError('Error al cargar proveedores');
        return of([]);
      }),
      finalize(() => this._isLoading.next(false))
    ).subscribe();
  }

  /**
   * Obtiene los productos suministrados por un proveedor concreto.
   * @param proveedorId ID del proveedor
   * @returns Observable con la lista de relaciones producto-proveedor
   */
  obtenerProductosDeProveedor(proveedorId: number): Observable<ProductoProveedorDTO[]> {
    return this.http.get<ProductoProveedorDTO[]>(`${this.ppUrl}/proveedor/${proveedorId}`).pipe(
      catchError(() => of([]))
    );
  }
}
