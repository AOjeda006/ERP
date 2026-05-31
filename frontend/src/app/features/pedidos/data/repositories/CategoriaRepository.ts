import { Categoria } from '../../domain/entities/categoria';
import { ICategoriaRepository } from '../../domain/interfaces/repositories/ICategoriaRepository';
import { CategoriaApiDataSource } from '../datasources/categoria-api.datasource';

/**
 * Repositorio de categorías que adapta {@link CategoriaApiDataSource} al contrato {@link ICategoriaRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link Categoria}.
 */
export class CategoriaRepository implements ICategoriaRepository {
  constructor(private readonly ds: CategoriaApiDataSource) {}

  /** @inheritdoc */
  async getAll(): Promise<Categoria[]> {
    const data = await this.ds.getAll();
    return data.map(d => new Categoria(d.categoriaID, d.nombreCategoria, d.descripcion));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<Categoria | null> {
    try {
      const d = await this.ds.getById(id);
      return new Categoria(d.categoriaID, d.nombreCategoria, d.descripcion);
    } catch {
      return null;
    }
  }

  /** @inheritdoc */
  async getActivos(): Promise<Categoria[]> {
    const data = await this.ds.getActivos();
    return data.map(d => new Categoria(d.categoriaID, d.nombreCategoria, d.descripcion));
  }
}
