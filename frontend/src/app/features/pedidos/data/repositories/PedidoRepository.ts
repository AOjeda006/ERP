import { Pedido } from '../../domain/entities/pedido';
import { DetallePedido } from '../../domain/entities/detalle_pedido';
import { IPedidoRepository } from '../../domain/interfaces/repositories/IPedidoRepository';
import { PedidoApiDataSource } from '../datasources/pedido-api.datasource';

/**
 * Repositorio de pedidos que adapta {@link PedidoApiDataSource} al contrato {@link IPedidoRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link Pedido} y {@link DetallePedido}.
 */
export class PedidoRepository implements IPedidoRepository {
  constructor(private readonly ds: PedidoApiDataSource) {}

  /**
   * Mapea un objeto JSON de la API a la entidad de dominio {@link Pedido}.
   * @param d Objeto JSON crudo devuelto por la API.
   * @returns Instancia de {@link Pedido} con sus líneas de detalle.
   * @internal
   */
  private toEntity(d: any): Pedido {
    const detalles: DetallePedido[] = Array.isArray(d.detalles)
      ? d.detalles.map(
          (x: any) =>
            new DetallePedido(
              x.detallePedidoID ?? 0,
              x.pedidoID ?? d.pedidoID ?? 0,
              x.productoID ?? 0,
              x.productoNombre ?? '',
              x.codigoProducto ?? '',
              x.cantidad ?? 0,
              x.precioUnitario ?? 0,
              x.descuento ?? 0,
              x.importeLinea ?? 0,
              x.observaciones ?? null
            )
        )
      : [];

    return new Pedido(
      d.pedidoID,
      d.numeroPedido,
      d.proveedorNombre,
      d.estadoNombre,
      new Date(d.fechaPedido),
      d.fechaEntregaPrevista ? new Date(d.fechaEntregaPrevista) : null,
      d.importeTotalConIVA,
      detalles
    );
  }

  /** @inheritdoc */
  async getAll(): Promise<Pedido[]> {
    const data = await this.ds.getAll();
    return data.map((d) => this.toEntity(d));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<Pedido | null> {
    try {
      const d = await this.ds.getById(id);
      return this.toEntity(d);
    } catch {
      return null;
    }
  }

  /** @inheritdoc */
  async create(body: any): Promise<Pedido> {
    const d = await this.ds.create(body);
    return this.toEntity(d);
  }

  /** @inheritdoc */
  update(id: number, body: any): Promise<void> {
    return this.ds.update(id, body);
  }

  /** @inheritdoc */
  delete(id: number): Promise<void> {
    return this.ds.delete(id);
  }

  /** @inheritdoc */
  async getByProveedor(proveedorId: number): Promise<Pedido[]> {
    const data = await this.ds.getByProveedor(proveedorId);
    return data.map((d) => this.toEntity(d));
  }

  /** @inheritdoc */
  async getByEstado(estadoId: number): Promise<Pedido[]> {
    const data = await this.ds.getByEstado(estadoId);
    return data.map((d) => this.toEntity(d));
  }
}
