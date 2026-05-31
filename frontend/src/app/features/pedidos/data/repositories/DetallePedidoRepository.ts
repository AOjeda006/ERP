import { DetallePedido } from '../../domain/entities/detalle_pedido';
import { IDetallePedidoRepository } from '../../domain/interfaces/repositories/IDetallePedidoRepository';
import { DetallePedidoApiDataSource } from '../datasources/detalle-pedido-api.datasource';

/**
 * Repositorio de líneas de detalle que adapta {@link DetallePedidoApiDataSource} al contrato {@link IDetallePedidoRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link DetallePedido}.
 */
export class DetallePedidoRepository implements IDetallePedidoRepository {
  constructor(private readonly ds: DetallePedidoApiDataSource) {}

  /**
   * Mapea un objeto JSON de la API a la entidad {@link DetallePedido}.
   * @param d Objeto JSON crudo devuelto por la API.
   * @internal
   */
  private toEntity(d: any): DetallePedido {
    return new DetallePedido(
      d.detallePedidoID ?? 0,
      d.pedidoID ?? 0,
      d.productoID ?? 0,
      d.productoNombre ?? '',
      d.codigoProducto ?? '',
      d.cantidad ?? 0,
      d.precioUnitario ?? 0,
      d.descuento ?? 0,
      d.importeLinea ?? 0,
      d.observaciones ?? null
    );
  }

  /** @inheritdoc */
  async getAll(): Promise<DetallePedido[]> {
    const data = await this.ds.getAll();
    return data.map((d) => this.toEntity(d));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<DetallePedido | null> {
    try {
      const d = await this.ds.getById(id);
      return this.toEntity(d);
    } catch {
      return null;
    }
  }

  /** @inheritdoc */
  async getByPedidoId(pedidoId: number): Promise<DetallePedido[]> {
    const data = await this.ds.getByPedidoId(pedidoId);
    return data.map((d) => this.toEntity(d));
  }

  /** @inheritdoc */
  async getByProductoId(productoId: number): Promise<DetallePedido[]> {
    const data = await this.ds.getByProductoId(productoId);
    return data.map((d) => this.toEntity(d));
  }

  /** @inheritdoc */
  getTotalPedido(pedidoId: number): Promise<number> {
    return this.ds.getTotalPedido(pedidoId);
  }
}
