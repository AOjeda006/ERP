import { DetallePedido } from '../../entities/detalle_pedido';

/**
 * Contrato del repositorio de líneas de detalle de pedido.
 * @remarks Implementado por {@link DetallePedidoRepository}.
 */
export interface IDetallePedidoRepository {
  /** Devuelve todas las líneas de detalle. */
  getAll(): Promise<DetallePedido[]>;
  /**
   * Obtiene una línea de detalle por su identificador.
   * @param id Identificador del detalle.
   * @returns Entidad {@link DetallePedido} o `null` si no existe.
   */
  getById(id: number): Promise<DetallePedido | null>;
  /**
   * Devuelve todas las líneas de detalle de un pedido.
   * @param pedidoId Identificador del pedido padre.
   */
  getByPedidoId(pedidoId: number): Promise<DetallePedido[]>;
  /**
   * Devuelve todas las líneas de detalle que contienen un producto específico.
   * @param productoId Identificador del producto.
   */
  getByProductoId(productoId: number): Promise<DetallePedido[]>;
  /**
   * Calcula el importe total de un pedido sumando sus líneas de detalle.
   * @param pedidoId Identificador del pedido.
   * @returns Importe total calculado.
   */
  getTotalPedido(pedidoId: number): Promise<number>;
}
