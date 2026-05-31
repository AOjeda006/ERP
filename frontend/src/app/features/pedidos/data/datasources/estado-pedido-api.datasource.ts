import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad EstadoPedido.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/EstadosPedido`.
 */
export class EstadoPedidoApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todos los estados de pedido (`GET /api/EstadosPedido`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('EstadosPedido');
  }

  /**
   * Obtiene un estado de pedido por su identificador (`GET /api/EstadosPedido/{id}`).
   * @param id Identificador del estado.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`EstadosPedido/${id}`);
  }
}
