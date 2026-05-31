import { Proveedor } from '../../domain/entities/proveedor';
import { IProveedorRepository } from '../../domain/interfaces/repositories/IProveedorRepository';
import { ProveedorApiDataSource } from '../datasources/proveeedor-api.datasource';

/**
 * Repositorio de proveedores que adapta {@link ProveedorApiDataSource} al contrato {@link IProveedorRepository}.
 * @remarks Convierte los objetos JSON de la API en entidades de dominio {@link Proveedor}.
 */
export class ProveedorRepository implements IProveedorRepository {
  constructor(private readonly ds: ProveedorApiDataSource) {}

  /**
   * Mapea un objeto JSON de la API a la entidad {@link Proveedor}.
   * @param d Objeto JSON crudo devuelto por la API.
   * @internal
   */
  private toEntity(d: any): Proveedor {
    return new Proveedor(
      d.proveedorID,
      d.cif,
      d.razonSocial,
      d.nombreComercial ?? null,
      d.telefono ?? null,
      d.email ?? null,
      d.personaContacto ?? null
    );
  }

  /** @inheritdoc */
  async getAll(): Promise<Proveedor[]> {
    const data = await this.ds.getAll();
    return data.map(d => this.toEntity(d));
  }

  /** @inheritdoc */
  async getActivos(): Promise<Proveedor[]> {
    const data = await this.ds.getActivos();
    return data.map(d => this.toEntity(d));
  }

  /** @inheritdoc */
  async getById(id: number): Promise<Proveedor | null> {
    try {
      const d = await this.ds.getById(id);
      return this.toEntity(d);
    } catch {
      return null;
    }
  }
}
