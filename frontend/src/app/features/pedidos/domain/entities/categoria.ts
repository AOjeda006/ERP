/** Entidad de dominio que representa una categoría de productos. */
export class Categoria {
  private readonly _categoriaID: number;
  private readonly _nombreCategoria: string;
  private readonly _descripcion: string;

  constructor(categoriaID: number, nombreCategoria: string, descripcion: string) {
    this._categoriaID = categoriaID;
    this._nombreCategoria = nombreCategoria;
    this._descripcion = descripcion;
  }

  /** @returns Identificador único de la categoría. */
  public get categoriaID(): number {
    return this._categoriaID;
  }

  /** @returns Nombre descriptivo de la categoría. */
  public get nombreCategoria(): string {
    return this._nombreCategoria;
  }

  /** @returns Descripción ampliada de la categoría. */
  public get descripcion(): string {
    return this._descripcion;
  }
}
