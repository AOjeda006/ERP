/**
 * DTO de producto devuelto por la API REST.
 * @remarks El campo `stockActual` sólo está disponible en la entidad de dominio;
 * el backend no lo incluye en `ProductoDTO` cuando se consultan productos de proveedor.
 */
export interface ProductoDTO {
  productoID: number;
  codigoProducto: string;
  nombreProducto: string;
  descripcion?: string | null;
  unidadMedida: string;
  precioUnitario?: number | null;
  stockActual: number;
  categoriaNombre: string;
}
