import { Categoria } from '../../entities/categoria';

/**
 * Contrato de los casos de uso de categoría.
 * @remarks Implementado por {@link CategoriaUseCases}.
 */
export interface ICategoriaUseCases {
  /** Devuelve todas las categorías. */
  getAllAsync(): Promise<Categoria[]>;
  /**
   * Obtiene una categoría por su identificador.
   * @param id Identificador de la categoría.
   * @returns Entidad {@link Categoria} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<Categoria | null>;
  /** Devuelve sólo las categorías activas. */
  getActivosAsync(): Promise<Categoria[]>;
}