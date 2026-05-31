import { EstadoPedido } from '../../entities/estado_pedido';

/**
 * Contrato del repositorio de estados de pedido.
 * @remarks Implementado por {@link EstadoPedidoRepository}.
 */
export interface IEstadoPedidoRepository {
  /** Devuelve todos los estados de pedido disponibles. */
  getAll(): Promise<EstadoPedido[]>;
  /**
   * Obtiene un estado de pedido por su identificador.
   * @param id Identificador del estado.
   * @returns Entidad {@link EstadoPedido} o `null` si no existe.
   */
  getById(id: number): Promise<EstadoPedido | null>;
}
