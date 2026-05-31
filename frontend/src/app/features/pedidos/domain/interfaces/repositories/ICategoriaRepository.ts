import { Categoria } from '../../entities/categoria';

/**
 * Contrato del repositorio de categorías de productos.
 * @remarks Implementado por {@link CategoriaRepository}.
 */
export interface ICategoriaRepository {
  /** Devuelve todas las categorías. */
  getAll(): Promise<Categoria[]>;
  /**
   * Obtiene una categoría por su identificador.
   * @param id Identificador de la categoría.
   * @returns Entidad {@link Categoria} o `null` si no existe.
   */
  getById(id: number): Promise<Categoria | null>;
  /** Devuelve únicamente las categorías activas. */
  getActivos(): Promise<Categoria[]>;
}
