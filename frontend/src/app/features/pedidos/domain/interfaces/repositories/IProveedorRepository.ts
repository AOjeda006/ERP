import { Proveedor } from '../../entities/proveedor';

/**
 * Contrato del repositorio de proveedores.
 * @remarks Implementado por {@link ProveedorRepository}.
 */
export interface IProveedorRepository {
  /** Devuelve todos los proveedores (activos e inactivos). */
  getAll(): Promise<Proveedor[]>;
  /** Devuelve únicamente los proveedores activos. */
  getActivos(): Promise<Proveedor[]>;
  /**
   * Obtiene un proveedor por su identificador.
   * @param id Identificador del proveedor.
   * @returns Entidad {@link Proveedor} o `null` si no existe.
   */
  getById(id: number): Promise<Proveedor | null>;
}
