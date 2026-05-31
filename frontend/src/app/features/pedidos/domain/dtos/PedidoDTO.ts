/** Payload de una línea de detalle al crear o editar un pedido. */
export interface DetallePedidoPayload {
  productoID: number;
  cantidad: number;
  precioUnitario: number;
  descuento: number;
}

/**
 * Payload para crear un nuevo pedido (`POST /api/Pedidos`).
 * @remarks `detalles` debe contener al menos una línea.
 */
export interface CreatePedidoPayload {
  proveedorID: number;
  fechaEntregaPrevista?: string | null;
  observaciones?: string | null;
  detalles: DetallePedidoPayload[];
}

/**
 * DTO de pedido devuelto por la API REST.
 * @remarks El listado (`GET /api/Pedidos`) devuelve `subtotal` y `numeroLineas`.
 * El detalle (`GET /api/Pedidos/{id}`) devuelve `proveedor{}`, `estado{}` y `detalles[]`.
 * Las fechas se reciben como cadenas ISO 8601.
 */
export interface PedidoDTO {
  pedidoID: number;
  numeroPedido: string;
  proveedorID: number;
  proveedorNombre?: string;
  estadoID?: number;
  estadoNombre?: string;
  fechaPedido: string;
  fechaEntregaPrevista?: string | null;
  fechaRecepcion?: string | null;
  importeTotal?: number;
  iva?: number;
  importeTotalConIVA?: number;
  subtotal?: number;
  numeroLineas?: number;
  observaciones?: string | null;
  creadoPor?: string;
  activo?: boolean;
  detalles?: DetallePedidoPayload[];
  proveedor?: { proveedorID: number; razonSocial: string; [key: string]: any };
  estado?: { estadoID: number; nombreEstado: string; [key: string]: any };
}