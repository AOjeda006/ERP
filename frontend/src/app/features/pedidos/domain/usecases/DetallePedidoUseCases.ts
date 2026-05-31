import { IDetallePedidoUseCases } from '../interfaces/usecases/IDetallePedidoUseCases';
import { IDetallePedidoRepository } from '../interfaces/repositories/IDetallePedidoRepository';
import { DetallePedido } from '../entities/detalle_pedido';

/**
 * Implementación de {@link IDetallePedidoUseCases}.
 * @remarks Delega todas las operaciones en {@link IDetallePedidoRepository}.
 */
export class DetallePedidoUseCases implements IDetallePedidoUseCases {
  constructor(private repo: IDetallePedidoRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<DetallePedido[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<DetallePedido | null> {
    return this.repo.getById(id);
  }

  /** @inheritdoc */
  getByPedidoIdAsync(pedidoId: number): Promise<DetallePedido[]> {
    return this.repo.getByPedidoId(pedidoId);
  }

  /** @inheritdoc */
  getByProductoIdAsync(productoId: number): Promise<DetallePedido[]> {
    return this.repo.getByProductoId(productoId);
  }

  /** @inheritdoc */
  getTotalPedidoAsync(pedidoId: number): Promise<number> {
    return this.repo.getTotalPedido(pedidoId);
  }
}
