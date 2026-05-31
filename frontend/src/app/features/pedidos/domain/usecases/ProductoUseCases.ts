import type { IProductoUseCases } from '../interfaces/usecases/IProductoUseCases';
import type { IProductoRepository } from '../interfaces/repositories/IProductoRepository';
import type { Producto } from '../entities/producto';

/**
 * Implementación de {@link IProductoUseCases}.
 * @remarks Delega todas las operaciones en {@link IProductoRepository}.
 */
export class ProductoUseCases implements IProductoUseCases {
  constructor(private repo: IProductoRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<Producto[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getActivosAsync(): Promise<Producto[]> {
    return this.repo.getActivos();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<Producto | null> {
    return this.repo.getById(id);
  }

  /** @inheritdoc */
  getByCategoriaAsync(categoriaId: number): Promise<Producto[]> {
    return this.repo.getByCategoria(categoriaId);
  }
}
