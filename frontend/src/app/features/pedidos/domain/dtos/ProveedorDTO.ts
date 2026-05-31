/**
 * DTO de proveedor devuelto por `GET /api/Proveedores`.
 * @remarks Las fechas (`fechaAlta`, `fechaModificacion`) se reciben como cadenas ISO 8601.
 */
export interface ProveedorDTO {
  proveedorID: number;
  cif: string;
  razonSocial: string;
  nombreComercial?: string | null;
  direccion?: string | null;
  codigoPostal?: string | null;
  ciudad?: string | null;
  provincia?: string | null;
  pais: string;
  telefono?: string | null;
  email?: string | null;
  personaContacto?: string | null;
  activo: boolean;
  /** Fecha de alta del proveedor en formato ISO 8601. */
  fechaAlta: string;
  fechaModificacion?: string | null;
}
