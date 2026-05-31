/**
 * Entidad de dominio que representa una línea de detalle de un pedido.
 * @remarks Incluye el importe ya calculado (`importeLinea`) y el descuento aplicado.
 */
export class DetallePedido {
  private readonly _detallePedidoID: number;
  private readonly _pedidoID: number;
  private readonly _productoID: number;
  private readonly _productoNombre: string;
  private readonly _codigoProducto: string;
  private readonly _cantidad: number;
  private readonly _precioUnitario: number;
  private readonly _descuento: number;
  private readonly _importeLinea: number;
  private readonly _observaciones: string | null;

  constructor(
    detallePedidoID: number,
    pedidoID: number,
    productoID: number,
    productoNombre: string,
    codigoProducto: string,
    cantidad: number,
    precioUnitario: number,
    descuento: number,
    importeLinea: number,
    observaciones: string | null = null
  ) {
    this._detallePedidoID = detallePedidoID;
    this._pedidoID = pedidoID;
    this._productoID = productoID;
    this._productoNombre = productoNombre;
    this._codigoProducto = codigoProducto;
    this._cantidad = cantidad;
    this._precioUnitario = precioUnitario;
    this._descuento = descuento;
    this._importeLinea = importeLinea;
    this._observaciones = observaciones;
  }

  /** @returns Identificador único de la línea de detalle. */
  public get detallePedidoID(): number {
    return this._detallePedidoID;
  }

  /** @returns Identificador del pedido al que pertenece esta línea. */
  public get pedidoID(): number {
    return this._pedidoID;
  }

  /** @returns Identificador del producto solicitado. */
  public get productoID(): number {
    return this._productoID;
  }

  /** @returns Nombre del producto en el momento de crear la línea. */
  public get productoNombre(): string {
    return this._productoNombre;
  }

  /** @returns Código interno del producto. */
  public get codigoProducto(): string {
    return this._codigoProducto;
  }

  /** @returns Cantidad de unidades solicitadas. */
  public get cantidad(): number {
    return this._cantidad;
  }

  /** @returns Precio unitario acordado con el proveedor. */
  public get precioUnitario(): number {
    return this._precioUnitario;
  }

  /** @returns Porcentaje de descuento aplicado (0–100). */
  public get descuento(): number {
    return this._descuento;
  }

  /** @returns Importe total de la línea tras aplicar descuento. */
  public get importeLinea(): number {
    return this._importeLinea;
  }

  /** @returns Observaciones de la línea o `null` si no las hay. */
  public get observaciones(): string | null {
    return this._observaciones;
  }
}
