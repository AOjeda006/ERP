import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad DetallePedido (líneas de pedido).
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/DetallePedido`.
 */
export class DetallePedidoApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todas las líneas de detalle (`GET /api/DetallePedido`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('DetallePedido');
  }

  /**
   * Obtiene una línea de detalle por su identificador (`GET /api/DetallePedido/{id}`).
   * @param id Identificador de la línea.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`DetallePedido/${id}`);
  }

  /**
   * Obtiene las líneas de un pedido concreto (`GET /api/DetallePedido/pedido/{pedidoId}`).
   * @param pedidoId Identificador del pedido.
   */
  getByPedidoId(pedidoId: number): Promise<any[]> {
    return this.get<any[]>(`DetallePedido/pedido/${pedidoId}`);
  }

  /**
   * Obtiene las líneas que contienen un producto (`GET /api/DetallePedido/producto/{productoId}`).
   * @param productoId Identificador del producto.
   */
  getByProductoId(productoId: number): Promise<any[]> {
    return this.get<any[]>(`DetallePedido/producto/${productoId}`);
  }

  /**
   * Calcula el importe total de un pedido (`GET /api/DetallePedido/pedido/{pedidoId}/total`).
   * @param pedidoId Identificador del pedido.
   * @returns Importe total calculado.
   */
  async getTotalPedido(pedidoId: number): Promise<number> {
    const res = await this.get<{ pedidoId: number; total: number }>(
      `DetallePedido/pedido/${pedidoId}/total`
    );
    return res.total;
  }

  /**
   * Crea una nueva línea de detalle (`POST /api/DetallePedido/pedido/{pedidoId}`).
   * @param pedidoId Identificador del pedido al que pertenece la línea.
   * @param body Payload de la línea de detalle.
   */
  create(pedidoId: number, body: any): Promise<any> {
    return this.post<any>(`DetallePedido/pedido/${pedidoId}`, body);
  }

  /**
   * Actualiza una línea de detalle (`PUT /api/DetallePedido`).
   * @param body Payload con el identificador incluido en el cuerpo.
   */
  update(body: any): Promise<any> {
    return this.put<any>('DetallePedido', body);
  }

  /**
   * Elimina una línea de detalle (`DELETE /api/DetallePedido/{detallePedidoId}`).
   * @param detallePedidoId Identificador de la línea.
   */
  delete(detallePedidoId: number): Promise<void> {
    return this.del<void>(`DetallePedido/${detallePedidoId}`);
  }
}