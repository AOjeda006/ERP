import type { IEstadoPedidoUseCases } from '../interfaces/usecases/IEstadoPedidoUseCases';
import type { IEstadoPedidoRepository } from '../interfaces/repositories/IEstadoPedidoRepository';
import type { EstadoPedido } from '../entities/estado_pedido';

/**
 * Implementación de {@link IEstadoPedidoUseCases}.
 * @remarks Delega todas las operaciones en {@link IEstadoPedidoRepository}.
 */
export class EstadoPedidoUseCases implements IEstadoPedidoUseCases {
  constructor(private repo: IEstadoPedidoRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<EstadoPedido[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<EstadoPedido | null> {
    return this.repo.getById(id);
  }
}
