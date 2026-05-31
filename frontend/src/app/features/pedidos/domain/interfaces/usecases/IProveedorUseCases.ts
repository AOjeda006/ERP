import type { Proveedor } from '../../entities/proveedor';

/**
 * Contrato de los casos de uso de proveedor.
 * @remarks Implementado por {@link ProveedorUseCases}.
 */
export interface IProveedorUseCases {
  /** Devuelve todos los proveedores. */
  getAllAsync(): Promise<Proveedor[]>;
  /** Devuelve sólo los proveedores activos. */
  getActivosAsync(): Promise<Proveedor[]>;
  /**
   * Obtiene un proveedor por su identificador.
   * @param id Identificador del proveedor.
   * @returns Entidad {@link Proveedor} o `null` si no existe.
   */
  getByIdAsync(id: number): Promise<Proveedor | null>;
}
