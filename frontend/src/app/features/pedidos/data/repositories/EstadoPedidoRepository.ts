import { EstadoPedido } from '../../domain/entities/estado_pedido';
import { IEstadoPedidoRepository } from '../../domain/interfaces/repositories/IEstadoPedidoRepository';
import { EstadoPedidoApiDataSource } from '../datasources/estado-pedido-api.datasource';

/**
 * Repositorio de estados de pedido que adapta {@link EstadoPedidoApiDataSource} al contrato {@link IEstadoPedidoRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link EstadoPedido}.
 */
export class EstadoPedidoRepository implements IEstadoPedidoRepository {
  constructor(private readonly ds: EstadoPedidoApiDataSource) {}

  /**
   * Mapea un objeto JSON de la API a la entidad {@link EstadoPedido}.
   * @param d Objeto JSON crudo devuelto por la API.
   * @internal
   */
  private toEntity(d: any): EstadoPedido {
    return new EstadoPedido(d.estadoID, d.nombreEstado, d.descripcion ?? null);
  }

  /** @inheritdoc */
  async getAll(): Promise<EstadoPedido[]> {
    const data = await this.ds.getAll();
    return data.map(d => this.toEntity(d));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<EstadoPedido | null> {
    try {
      const d = await this.ds.getById(id);
      return this.toEntity(d);
    } catch {
      return null;
    }
  }
}
