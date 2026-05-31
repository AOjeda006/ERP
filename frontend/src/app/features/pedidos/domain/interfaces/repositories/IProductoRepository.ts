import type { Producto } from '../../entities/producto';

/**
 * Contrato del repositorio de productos.
 * @remarks Implementado por {@link ProductoRepository}.
 */
export interface IProductoRepository {
  /** Devuelve todos los productos del catálogo. */
  getAll(): Promise<Producto[]>;
  /** Devuelve únicamente los productos activos. */
  getActivos(): Promise<Producto[]>;
  /**
   * Obtiene un producto por su identificador.
   * @param id Identificador del producto.
   * @returns Entidad {@link Producto} o `null` si no existe.
   */
  getById(id: number): Promise<Producto | null>;
  /**
   * Filtra productos por categoría.
   * @param categoriaId Identificador de la categoría.
   */
  getByCategoria(categoriaId: number): Promise<Producto[]>;
}
