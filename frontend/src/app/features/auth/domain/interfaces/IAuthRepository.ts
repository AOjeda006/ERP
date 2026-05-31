import { Usuario } from '../entities/usuario.entity';

/**
 * Contrato del repositorio de autenticación.
 * @remarks Implementado por {@link AuthRepository} usando Firebase Auth como datasource.
 */
export interface IAuthRepository {
  /**
   * Autentica al usuario con email y contraseña.
   * @param email Correo electrónico del usuario.
   * @param password Contraseña en texto plano.
   * @returns Entidad {@link Usuario} con los datos del usuario autenticado.
   * @throws {Error} Si las credenciales son incorrectas o el servicio falla.
   */
  login(email: string, password: string): Promise<Usuario>;

  /** Cierra la sesión del usuario actualmente autenticado en Firebase. */
  logout(): Promise<void>;

  /**
   * Obtiene el usuario actualmente autenticado.
   * @returns Entidad {@link Usuario} o `null` si no hay sesión activa.
   */
  getCurrentUser(): Promise<Usuario | null>;

  /**
   * Comprueba si existe una sesión activa.
   * @returns `true` si hay un usuario autenticado.
   */
  isAuthenticated(): Promise<boolean>;
}