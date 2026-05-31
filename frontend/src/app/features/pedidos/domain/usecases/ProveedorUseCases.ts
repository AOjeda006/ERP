import type { IProveedorUseCases } from '../interfaces/usecases/IProveedorUseCases';
import type { IProveedorRepository } from '../interfaces/repositories/IProveedorRepository';
import type { Proveedor } from '../entities/proveedor';

/**
 * Implementación de {@link IProveedorUseCases}.
 * @remarks Delega todas las operaciones en {@link IProveedorRepository}.
 */
export class ProveedorUseCases implements IProveedorUseCases {
  constructor(private repo: IProveedorRepository) {}

  /** @inheritdoc */
  getAllAsync(): Promise<Proveedor[]> {
    return this.repo.getAll();
  }

  /** @inheritdoc */
  getActivosAsync(): Promise<Proveedor[]> {
    return this.repo.getActivos();
  }

  /** @inheritdoc */
  getByIdAsync(id: number): Promise<Proveedor | null> {
    return this.repo.getById(id);
  }
}
