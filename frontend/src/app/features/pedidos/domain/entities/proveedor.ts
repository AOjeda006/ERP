/** Entidad de dominio que representa un proveedor registrado en el sistema. */
export class Proveedor {
  private readonly _proveedorID: number;
  private readonly _cif: string;
  private readonly _razonSocial: string;
  private readonly _nombreComercial: string | null;
  private readonly _telefono: string | null;
  private readonly _email: string | null;
  private readonly _personaContacto: string | null;

  constructor(
    proveedorID: number,
    cif: string,
    razonSocial: string,
    nombreComercial: string | null,
    telefono: string | null,
    email: string | null,
    personaContacto: string | null
  ) {
    this._proveedorID = proveedorID;
    this._cif = cif;
    this._razonSocial = razonSocial;
    this._nombreComercial = nombreComercial;
    this._telefono = telefono;
    this._email = email;
    this._personaContacto = personaContacto;
  }

  /** @returns Identificador único del proveedor. */
  public get proveedorID(): number {
    return this._proveedorID;
  }

  /** @returns CIF fiscal del proveedor. */
  public get cif(): string {
    return this._cif;
  }

  /** @returns Razón social (nombre legal) del proveedor. */
  public get razonSocial(): string {
    return this._razonSocial;
  }

  /** @returns Nombre comercial del proveedor o `null` si no está registrado. */
  public get nombreComercial(): string | null {
    return this._nombreComercial;
  }

  /** @returns Teléfono de contacto o `null` si no está registrado. */
  public get telefono(): string | null {
    return this._telefono;
  }

  /** @returns Dirección de email o `null` si no está registrada. */
  public get email(): string | null {
    return this._email;
  }

  /** @returns Nombre de la persona de contacto o `null` si no está registrado. */
  public get personaContacto(): string | null {
    return this._personaContacto;
  }
}
