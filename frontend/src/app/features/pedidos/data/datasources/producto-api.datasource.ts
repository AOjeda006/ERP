import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad Producto.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/Productos`.
 */
export class ProductoApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todos los productos (`GET /api/Productos`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('Productos');
  }

  /** Obtiene los productos activos (`GET /api/Productos/activos`). */
  getActivos(): Promise<any[]> {
    return this.get<any[]>('Productos/activos');
  }

  /**
   * Obtiene un producto por su identificador (`GET /api/Productos/{id}`).
   * @param id Identificador del producto.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`Productos/${id}`);
  }

  /**
   * Filtra productos por categoría (`GET /api/Productos/categoria/{categoriaId}`).
   * @param categoriaId Identificador de la categoría.
   */
  getByCategoria(categoriaId: number): Promise<any[]> {
    return this.get<any[]>(`Productos/categoria/${categoriaId}`);
  }

  /**
   * Crea un nuevo producto (`POST /api/Productos`).
   * @param body Payload de creación.
   */
  create(body: any): Promise<any> {
    return this.post<any>('Productos', body);
  }

  /**
   * Actualiza un producto (`PUT /api/Productos/{id}`).
   * @param id Identificador del producto.
   * @param body Payload de actualización.
   */
  update(id: number, body: any): Promise<any> {
    return this.put<any>(`Productos/${id}`, body);
  }

  /**
   * Elimina un producto (`DELETE /api/Productos/{id}`).
   * @param id Identificador del producto.
   */
  delete(id: number): Promise<void> {
    return this.del<void>(`Productos/${id}`);
  }
}