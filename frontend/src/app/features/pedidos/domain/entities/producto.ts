/**
 * Entidad de dominio que representa un producto del catálogo.
 * @remarks Todos los atributos son inmutables (readonly); el stock sólo es legible
 * desde TypeScript (la escritura ocurre en el backend).
 */
export class Producto {
  private readonly _productoID: number;
  private readonly _codigoProducto: string;
  private readonly _nombreProducto: string;
  private readonly _descripcion: string | null;
  private readonly _unidadMedida: string;
  private readonly _precioUnitario: number | null;
  private readonly _stockActual: number;
  private readonly _categoriaNombre: string;

  constructor(
    productoID: number,
    codigoProducto: string,
    nombreProducto: string,
    descripcion: string | null,
    unidadMedida: string,
    precioUnitario: number | null,
    stockActual: number,
    categoriaNombre: string
  ) {
    this._productoID = productoID;
    this._codigoProducto = codigoProducto;
    this._nombreProducto = nombreProducto;
    this._descripcion = descripcion;
    this._unidadMedida = unidadMedida;
    this._precioUnitario = precioUnitario;
    this._stockActual = stockActual;
    this._categoriaNombre = categoriaNombre;
  }

  /** @returns Identificador único del producto. */
  public get productoID(): number {
    return this._productoID;
  }

  /** @returns Código interno del producto (p. ej. "PROD-001"). */
  public get codigoProducto(): string {
    return this._codigoProducto;
  }

  /** @returns Nombre comercial del producto. */
  public get nombreProducto(): string {
    return this._nombreProducto;
  }

  /** @returns Descripción detallada o `null` si no está definida. */
  public get descripcion(): string | null {
    return this._descripcion;
  }

  /** @returns Unidad de medida del producto (p. ej. "ud", "kg", "L"). */
  public get unidadMedida(): string {
    return this._unidadMedida;
  }

  /** @returns Precio unitario de catálogo o `null` si no está fijado. */
  public get precioUnitario(): number | null {
    return this._precioUnitario;
  }

  /** @returns Stock disponible en almacén según el último dato del backend. */
  public get stockActual(): number {
    return this._stockActual;
  }

  /** @returns Nombre de la categoría a la que pertenece el producto. */
  public get categoriaNombre(): string {
    return this._categoriaNombre;
  }
}
