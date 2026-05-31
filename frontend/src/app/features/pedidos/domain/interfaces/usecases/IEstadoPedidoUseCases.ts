import { EstadoPedido } from '../../entities/estado_pedido';

/**
 * Contrato de los casos de uso de estado de pedido.
 * @remarks Implementado por {@link EstadoPedidoUseCases}.
 */
export interface IEstadoPedidoUseCases {
  /** Devuelve todos los estados de pedido disponibles. */
  getAllAsync(): Promise<EstadoPedido[]>;
  /**
   * Obtiene un estado de pedido por su identificador.
   * @param id Identificador del estado.
   * @returns Entidad {@link EstadoPedido} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<EstadoPedido | null>;
}
