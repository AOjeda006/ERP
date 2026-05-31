import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad Categoria.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/Categorias`.
 */
export class CategoriaApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todas las categorías (`GET /api/Categorias`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('Categorias');
  }

  /**
   * Obtiene una categoría por su identificador (`GET /api/Categorias/{id}`).
   * @param id Identificador de la categoría.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`Categorias/${id}`);
  }

  /** Obtiene las categorías activas (`GET /api/Categorias/activos`). */
  getActivos(): Promise<any[]> {
    return this.get<any[]>('Categorias/activos');
  }
}
