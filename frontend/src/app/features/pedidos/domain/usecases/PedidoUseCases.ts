import { IPedidoUseCases } from '../interfaces/usecases/IPedidoUseCases';
import { IPedidoRepository } from '../interfaces/repositories/IPedidoRepository';
import { Pedido } from '../entities/pedido';

/**
 * Implementación de {@link IPedidoUseCases}.
 * @remarks Delega todas las operaciones en {@link IPedidoRepository}.
 */
export class PedidoUseCases implements IPedidoUseCases {
  constructor(private repo: IPedidoRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<Pedido[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<Pedido | null> {
    return this.repo.getById(id);
  }

  /** @inheritdoc */
  createAsync(body: any): Promise<Pedido> {
    return this.repo.create(body);
  }

  /** @inheritdoc */
  updateAsync(id: number, body: any): Promise<void> {
    return this.repo.update(id, body);
  }

  /** @inheritdoc */
  deleteAsync(id: number): Promise<void> {
    return this.repo.delete(id);
  }

  /** @inheritdoc */
  getByProveedorAsync(proveedorId: number): Promise<Pedido[]> {
    return this.repo.getByProveedor(proveedorId);
  }

  /** @inheritdoc */
  getByEstadoAsync(estadoId: number): Promise<Pedido[]> {
    return this.repo.getByEstado(estadoId);
  }
}
