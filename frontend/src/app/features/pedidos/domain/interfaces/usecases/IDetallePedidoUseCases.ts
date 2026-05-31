import type { DetallePedido } from '../../entities/detalle_pedido';

/**
 * Contrato de los casos de uso de detalle de pedido.
 * @remarks Implementado por {@link DetallePedidoUseCases}.
 */
export interface IDetallePedidoUseCases {
  /** Devuelve todas las líneas de detalle. */
  getAllAsync(): Promise<DetallePedido[]>;
  /**
   * Obtiene una línea de detalle por su identificador.
   * @param id Identificador del detalle.
   * @returns Entidad {@link DetallePedido} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<DetallePedido | null>;
  /**
   * Devuelve las líneas de un pedido concreto.
   * @param pedidoId Identificador del pedido padre.
   */
  getByPedidoIdAsync(pedidoId: number): Promise<DetallePedido[]>;
  /**
   * Devuelve las líneas que contienen un producto concreto.
   * @param productoId Identificador del producto.
   */
  getByProductoIdAsync(productoId: number): Promise<DetallePedido[]>;
  /**
   * Calcula el importe total de un pedido.
   * @param pedidoId Identificador del pedido.
   * @returns Importe total calculado a partir de sus líneas.
   */
  getTotalPedidoAsync(pedidoId: number): Promise<number>;
}
