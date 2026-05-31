import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad Pedido.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/Pedidos`.
 */
export class PedidoApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todos los pedidos (`GET /api/Pedidos`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('Pedidos');
  }

  /**
   * Obtiene un pedido por su identificador (`GET /api/Pedidos/{id}`).
   * @param id Identificador del pedido.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`Pedidos/${id}`);
  }

  /**
   * Crea un nuevo pedido (`POST /api/Pedidos`).
   * @param body Payload de creación (`CreatePedidoPayload`).
   */
  create(body: any): Promise<any> {
    return this.post<any>('Pedidos', body);
  }

  /**
   * Actualiza un pedido (`PUT /api/Pedidos/{id}`).
   * @param id Identificador del pedido.
   * @param body Payload de actualización.
   */
  update(id: number, body: any): Promise<void> {
    return this.put<void>(`Pedidos/${id}`, body);
  }

  /**
   * Cambia el estado de un pedido (`PATCH /api/Pedidos/{id}/estado`).
   * @param id Identificador del pedido.
   * @param nuevoEstadoID Identificador del nuevo estado.
   */
  cambiarEstado(id: number, nuevoEstadoID: number): Promise<void> {
    return this.patch<void>(`Pedidos/${id}/estado`, { nuevoEstadoID });
  }

  /**
   * Elimina un pedido (`DELETE /api/Pedidos/{id}`).
   * @param id Identificador del pedido.
   */
  delete(id: number): Promise<void> {
    return this.del<void>(`Pedidos/${id}`);
  }

  /**
   * Filtra pedidos por proveedor (`GET /api/Pedidos/proveedor/{proveedorId}`).
   * @param proveedorId Identificador del proveedor.
   */
  getByProveedor(proveedorId: number): Promise<any[]> {
    return this.get<any[]>(`Pedidos/proveedor/${proveedorId}`);
  }

  /**
   * Filtra pedidos por estado (`GET /api/Pedidos/estado/{estadoId}`).
   * @param estadoId Identificador del estado.
   */
  getByEstado(estadoId: number): Promise<any[]> {
    return this.get<any[]>(`Pedidos/estado/${estadoId}`);
  }

  /** Obtiene los pedidos recibidos (`GET /api/Pedidos/recibidos`). */
  getRecibidos(): Promise<any[]> {
    return this.get<any[]>('Pedidos/recibidos');
  }
}
