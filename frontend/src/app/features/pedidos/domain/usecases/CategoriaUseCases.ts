import { ICategoriaUseCases } from '../interfaces/usecases/ICategoriaUseCases';
import { ICategoriaRepository } from '../interfaces/repositories/ICategoriaRepository';
import { Categoria } from '../entities/categoria';

/**
 * Implementación de {@link ICategoriaUseCases}.
 * @remarks Delega todas las operaciones en {@link ICategoriaRepository}.
 */
export class CategoriaUseCases implements ICategoriaUseCases {
  constructor(private repo: ICategoriaRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<Categoria[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<Categoria | null> {
    return this.repo.getById(id);
  }

  /** @inheritdoc */
  getActivosAsync(): Promise<Categoria[]> {
    return this.repo.getActivos();
  }
}
