import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, finalize, tap } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { NotificationService } from '../../../../core/services/notification.service';
import type { PedidoDTO } from '../../domain/dtos/PedidoDTO';

/**
 * ViewModel principal de pedidos: estado global, CRUD y cambio de estado.
 * @remarks Registrado como `providedIn:'root'` Y en el Container manual; ambas
 * instancias coexisten. Los componentes deben usar Angular DI (inject) para
 * acceder a la instancia correcta.
 * @see {@link PedidosListComponent}
 * @see {@link PedidosFormComponent}
 */
@Injectable({ providedIn: 'root' })
export class PedidosViewModel {
  private readonly http  = inject(HttpClient);
  public  readonly notify = inject(NotificationService);
  private readonly url   = `${environment.apiUrl}/Pedidos`;

  private _pedidos   = new BehaviorSubject<PedidoDTO[]>([]);
  pedidos$           = this._pedidos.asObservable();

  private _estados   = new BehaviorSubject<any[]>([]);
  estados$           = this._estados.asObservable();

  private _recibidos = new BehaviorSubject<PedidoDTO[]>([]);
  recibidos$         = this._recibidos.asObservable();

  private _productos = new BehaviorSubject<any[]>([]);
  /** Productos del proveedor seleccionado en el formulario de pedido. */
  productos$         = this._productos.asObservable();

  /** Observable de un solo disparo que lista todos los proveedores. */
  proveedores$ = this.http.get<any[]>(`${environment.apiUrl}/Proveedores`);

  isLoading$ = new BehaviorSubject<boolean>(false);
  isSaving$  = new BehaviorSubject<boolean>(false);

  /**
   * Actualiza la lista de productos del proveedor activo en el formulario.
   * @param data Array de productos normalizados
   */
  setProductos(data: any[]): void {
    this._productos.next(data ?? []);
  }

  /**
   * Obtiene los productos asociados a un proveedor.
   * @param proveedorId ID del proveedor
   * @returns Observable con los productos del proveedor
   */
  getProductosPorProveedor(proveedorId: number): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/ProductosProveedores/proveedor/${proveedorId}`);
  }

  /**
   * Carga todos los pedidos y los emite en {@link pedidos$}.
   * @remarks Actualiza {@link isLoading$} durante la petición.
   */
  cargarPedidos(): void {
    this.isLoading$.next(true);
    this.http.get<PedidoDTO[]>(this.url).pipe(
      finalize(() => this.isLoading$.next(false))
    ).subscribe(d => this._pedidos.next(d ?? []));
  }

  /**
   * Carga los estados de pedido disponibles y los emite en {@link estados$}.
   */
  cargarEstados(): void {
    this.http.get<any[]>(`${environment.apiUrl}/EstadosPedido`)
      .subscribe(d => this._estados.next(d ?? []));
  }

  /**
   * Carga los pedidos en estado Recibido y los emite en {@link recibidos$}.
   * @remarks Usado por el dashboard y el módulo de almacén.
   */
  cargarRecibidos(): void {
    this.http.get<PedidoDTO[]>(`${this.url}/recibidos`)
      .subscribe(d => this._recibidos.next(d ?? []));
  }

  /**
   * Obtiene los detalles completos de un pedido por ID.
   * @param id ID del pedido
   * @returns Observable con el pedido y sus líneas de detalle
   */
  obtenerPedidoPorId(id: number): Observable<PedidoDTO> {
    return this.http.get<PedidoDTO>(`${this.url}/${id}`);
  }

  /**
   * Cambia el estado de un pedido mediante PATCH.
   * @param pedido Pedido cuyo estado se actualiza
   * @param nuevoEstadoId ID del nuevo estado
   * @returns Observable que completa cuando el servidor confirma el cambio
   */
  cambiarEstado(pedido: PedidoDTO, nuevoEstadoId: number): Observable<any> {
    const payload = { nuevoEstadoID: nuevoEstadoId };
    this.isSaving$.next(true);
    return this.http.patch(`${this.url}/${(pedido as any).pedidoID}/estado`, payload).pipe(
      tap(() => {
        this.notify.showSuccess('Estado actualizado');
        this.cargarPedidos();
        this.cargarRecibidos();
      }),
      finalize(() => this.isSaving$.next(false))
    );
  }

  /**
   * Crea un nuevo pedido en el backend.
   * @param p Payload con los datos del pedido y sus líneas
   * @returns Observable que completa cuando el servidor confirma la creación
   */
  crearPedido(p: any): Observable<any> {
    this.isSaving$.next(true);
    return this.http.post(this.url, p).pipe(
      tap(() => {
        this.notify.showSuccess('Pedido creado exitosamente');
        this.cargarPedidos();
      }),
      finalize(() => this.isSaving$.next(false))
    );
  }

  /**
   * Actualiza un pedido existente (solo observaciones y líneas).
   * @param id ID del pedido a actualizar
   * @param p Payload con los nuevos datos
   * @returns Observable que completa cuando el servidor confirma la actualización
   */
  actualizarPedido(id: number, p: any): Observable<any> {
    this.isSaving$.next(true);
    return this.http.put(`${this.url}/${id}`, p).pipe(
      tap(() => {
        this.notify.showSuccess('Pedido actualizado correctamente');
        this.cargarPedidos();
      }),
      finalize(() => this.isSaving$.next(false))
    );
  }

  /**
   * Elimina un pedido del backend.
   * @param id ID del pedido a eliminar
   * @returns Observable que completa cuando el servidor confirma la eliminación
   */
  eliminarPedido(id: number): Observable<any> {
    return this.http.delete(`${this.url}/${id}`).pipe(
      tap(() => {
        this.notify.showInfo('Pedido eliminado');
        this.cargarPedidos();
      })
    );
  }
}
