import { HttpClient } from '@angular/common/http';
import { BaseApiDataSource } from './base-api.datasource';

/**
 * Datasource HTTP para la relación ProductoProveedor.
 * @remarks Extiende {@link BaseApiDataSource} y consume el recurso `/api/ProductosProveedores`.
 * Se usa para cargar los productos disponibles de un proveedor concreto en el formulario de pedidos.
 */
export class ProductoProveedorApiDataSource extends BaseApiDataSource {
  constructor(http: HttpClient) { super(http); }

  /**
   * Obtiene los productos asociados a un proveedor (`GET /api/ProductosProveedores/proveedor/{proveedorId}`).
   * @param proveedorId Identificador del proveedor.
   */
  getByProveedor(proveedorId: number): Promise<any[]> {
    return this.get<any[]>(`ProductosProveedores/proveedor/${proveedorId}`);
  }

  /**
   * Obtiene los proveedores asociados a un producto (`GET /api/ProductosProveedores/producto/{productoId}`).
   * @param productoId Identificador del producto.
   */
  getByProducto(productoId: number): Promise<any[]> {
    return this.get<any[]>(`ProductosProveedores/producto/${productoId}`);
  }
}
