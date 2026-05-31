import { DetallePedido } from './detalle_pedido';

/**
 * Entidad de dominio que representa un pedido a proveedor.
 * @remarks Contiene las líneas de detalle como colección inmutable de {@link DetallePedido}.
 */
export class Pedido {
  private readonly _pedidoID: number;
  private readonly _numeroPedido: string;
  private readonly _proveedorNombre: string;
  private readonly _estadoNombre: string;
  private readonly _fechaPedido: Date;
  private readonly _fechaEntregaPrevista: Date | null;
  private readonly _importeTotalConIVA: number;
  private readonly _detalles: DetallePedido[];

  constructor(
    pedidoID: number,
    numeroPedido: string,
    proveedorNombre: string,
    estadoNombre: string,
    fechaPedido: Date,
    fechaEntregaPrevista: Date | null,
    importeTotalConIVA: number,
    detalles: DetallePedido[]
  ) {
    this._pedidoID = pedidoID;
    this._numeroPedido = numeroPedido;
    this._proveedorNombre = proveedorNombre;
    this._estadoNombre = estadoNombre;
    this._fechaPedido = fechaPedido;
    this._fechaEntregaPrevista = fechaEntregaPrevista;
    this._importeTotalConIVA = importeTotalConIVA;
    this._detalles = detalles;
  }

  /** @returns Identificador único del pedido. */
  public get pedidoID(): number {
    return this._pedidoID;
  }

  /** @returns Número de pedido generado por el sistema (p. ej. "PED-2024-0001"). */
  public get numeroPedido(): string {
    return this._numeroPedido;
  }

  /** @returns Razón social del proveedor al que se realizó el pedido. */
  public get proveedorNombre(): string {
    return this._proveedorNombre;
  }

  /** @returns Nombre del estado actual del pedido (p. ej. "Pendiente", "Recibido"). */
  public get estadoNombre(): string {
    return this._estadoNombre;
  }

  /** @returns Fecha en que se realizó el pedido. */
  public get fechaPedido(): Date {
    return this._fechaPedido;
  }

  /** @returns Fecha prevista de entrega o `null` si no se especificó. */
  public get fechaEntregaPrevista(): Date | null {
    return this._fechaEntregaPrevista;
  }

  /** @returns Importe total del pedido con IVA incluido. */
  public get importeTotalConIVA(): number {
    return this._importeTotalConIVA;
  }

  /** @returns Colección inmutable de líneas de detalle del pedido. */
  public get detalles(): DetallePedido[] {
    return this._detalles;
  }
}
