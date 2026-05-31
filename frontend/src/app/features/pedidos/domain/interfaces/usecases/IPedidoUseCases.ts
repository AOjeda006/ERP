import type { Pedido } from '../../entities/pedido';

/**
 * Contrato de los casos de uso de pedido.
 * @remarks Implementado por {@link PedidoUseCases}.
 */
export interface IPedidoUseCases {
  /** Devuelve todos los pedidos disponibles. */
  getAllAsync(): Promise<Pedido[]>;
  /**
   * Obtiene un pedido por su identificador.
   * @param id Identificador del pedido.
   * @returns Entidad {@link Pedido} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<Pedido | null>;
  /**
   * Crea un nuevo pedido.
   * @param body Payload de creación.
   * @returns Entidad {@link Pedido} recién creada.
   */
  createAsync(body: any): Promise<Pedido>;
  /**
   * Actualiza un pedido existente.
   * @param id Identificador del pedido.
   * @param body Payload de actualización.
   */
  updateAsync(id: number, body: any): Promise<void>;
  /**
   * Elimina un pedido.
   * @param id Identificador del pedido.
   */
  deleteAsync(id: number): Promise<void>;
  /**
   * Filtra los pedidos por proveedor.
   * @param proveedorId Identificador del proveedor.
   */
  getByProveedorAsync(proveedorId: number): Promise<Pedido[]>;
  /**
   * Filtra los pedidos por estado.
   * @param estadoId Identificador del estado.
   */
  getByEstadoAsync(estadoId: number): Promise<Pedido[]>;
}
