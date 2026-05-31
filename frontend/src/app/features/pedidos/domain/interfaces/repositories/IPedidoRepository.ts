import type { Pedido } from '../../entities/pedido';

/**
 * Contrato del repositorio de pedidos.
 * @remarks Implementado por {@link PedidoRepository} usando la API REST como datasource.
 */
export interface IPedidoRepository {
  /** Devuelve todos los pedidos disponibles. */
  getAll(): Promise<Pedido[]>;
  /**
   * Obtiene un pedido por su identificador.
   * @param id Identificador del pedido.
   * @returns Entidad {@link Pedido} o `null` si no existe.
   */
  getById(id: number): Promise<Pedido | null>;
  /**
   * Crea un nuevo pedido.
   * @param body Payload de creación (`CreatePedidoPayload`).
   * @returns Entidad {@link Pedido} recién creada.
   */
  create(body: any): Promise<Pedido>;
  /**
   * Actualiza un pedido existente.
   * @param id Identificador del pedido.
   * @param body Payload de actualización parcial o total.
   */
  update(id: number, body: any): Promise<void>;
  /**
   * Elimina un pedido por su identificador.
   * @param id Identificador del pedido.
   */
  delete(id: number): Promise<void>;
  /**
   * Filtra los pedidos por proveedor.
   * @param proveedorId Identificador del proveedor.
   */
  getByProveedor(proveedorId: number): Promise<Pedido[]>;
  /**
   * Filtra los pedidos por estado.
   * @param estadoId Identificador del estado.
   */
  getByEstado(estadoId: number): Promise<Pedido[]>;
}
