import type { Producto } from '../../entities/producto';

/**
 * Contrato de los casos de uso de producto.
 * @remarks Implementado por {@link ProductoUseCases}.
 */
export interface IProductoUseCases {
  /** Devuelve todos los productos del catálogo. */
  getAllAsync(): Promise<Producto[]>;
  /** Devuelve sólo los productos activos. */
  getActivosAsync(): Promise<Producto[]>;
  /**
   * Obtiene un producto por su identificador.
   * @param id Identificador del producto.
   * @returns Entidad {@link Producto} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<Producto | null>;
  /**
   * Filtra productos por categoría.
   * @param categoriaId Identificador de la categoría.
   */
  getByCategoriaAsync(categoriaId: number): Promise<Producto[]>;
}
