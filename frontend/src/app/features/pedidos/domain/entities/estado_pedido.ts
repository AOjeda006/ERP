/** Entidad de dominio que representa un estado posible de un pedido (p. ej. Pendiente, Enviado). */
export class EstadoPedido {
  private readonly _estadoID: number;
  private readonly _nombreEstado: string;
  private readonly _descripcion: string | null;

  constructor(estadoID: number, nombreEstado: string, descripcion: string | null) {
    this._estadoID = estadoID;
    this._nombreEstado = nombreEstado;
    this._descripcion = descripcion;
  }

  /** @returns Identificador único del estado. */
  public get estadoID(): number {
    return this._estadoID;
  }

  /** @returns Nombre del estado (p. ej. "Pendiente", "Enviado", "Recibido"). */
  public get nombreEstado(): string {
    return this._nombreEstado;
  }

  /** @returns Descripción del estado o `null` si no está definida. */
  public get descripcion(): string | null {
    return this._descripcion;
  }
}
