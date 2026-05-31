import { Producto } from '../../domain/entities/producto';
import { IProductoRepository } from '../../domain/interfaces/repositories/IProductoRepository';
import { ProductoApiDataSource } from '../datasources/producto-api.datasource';

/**
 * Repositorio de productos que adapta {@link ProductoApiDataSource} al contrato {@link IProductoRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link Producto}.
 */
export class ProductoRepository implements IProductoRepository {
  constructor(private readonly ds: ProductoApiDataSource) {}

  /**
   * Mapea un objeto JSON de la API a la entidad {@link Producto}.
   * @param d Objeto JSON crudo devuelto por la API.
   * @internal
   */
  private toEntity(d: any): Producto {
    return new Producto(
      d.productoID,
      d.codigoProducto,
      d.nombreProducto,
      d.descripcion ?? null,
      d.unidadMedida,
      d.precioUnitario ?? null,
      d.stockActual ?? 0,
      d.categoriaNombre
    );
  }

  /** @inheritdoc */
  async getAll(): Promise<Producto[]> {
    const data = await this.ds.getAll();
    return data.map(d => this.toEntity(d));
  }

  /** @inheritdoc */
  async getActivos(): Promise<Producto[]> {
    const data = await this.ds.getActivos();
    return data.map(d => this.toEntity(d));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<Producto | null> {
    try {
      const d = await this.ds.getById(id);
      return this.toEntity(d);
    } catch {
      return null;
    }
  }

  /** @inheritdoc */
  async getByCategoria(categoriaId: number): Promise<Producto[]> {
    const data = await this.ds.getByCategoria(categoriaId);
    return data.map(d => this.toEntity(d));
  }
}
