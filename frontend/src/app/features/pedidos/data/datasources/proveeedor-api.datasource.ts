import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la entidad Proveedor.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/Proveedores`.
 */
export class ProveedorApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /** Obtiene todos los proveedores (`GET /api/Proveedores`). */
  getAll(): Promise<any[]> {
    return this.get<any[]>('Proveedores');
  }

  /** Obtiene los proveedores activos (`GET /api/Proveedores/activos`). */
  getActivos(): Promise<any[]> {
    return this.get<any[]>('Proveedores/activos');
  }

  /**
   * Obtiene un proveedor por su identificador (`GET /api/Proveedores/{id}`).
   * @param id Identificador del proveedor.
   */
  getById(id: number): Promise<any> {
    return this.get<any>(`Proveedores/${id}`);
  }

  /**
   * Crea un nuevo proveedor (`POST /api/Proveedores`).
   * @param body Payload de creación.
   */
  create(body: any): Promise<any> {
    return this.post<any>('Proveedores', body);
  }

  /**
   * Actualiza un proveedor (`PUT /api/Proveedores/{id}`).
   * @param id Identificador del proveedor.
   * @param body Payload de actualización.
   */
  update(id: number, body: any): Promise<any> {
    return this.put<any>(`Proveedores/${id}`, body);
  }

  /**
   * Elimina un proveedor (`DELETE /api/Proveedores/{id}`).
   * @param id Identificador del proveedor.
   */
  delete(id: number): Promise<void> {
    return this.del<void>(`Proveedores/${id}`);
  }
}