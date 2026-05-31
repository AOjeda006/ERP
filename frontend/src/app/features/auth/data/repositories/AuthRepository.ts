import { IAuthRepository } from '../../domain/interfaces/IAuthRepository';
import { Usuario } from '../../domain/entities/usuario.entity';
import { FirebaseAuthDataSource } from '../datasources/FirebaseAuthDataSource';

/**
 * Repositorio de autenticación que adapta {@link FirebaseAuthDataSource} al contrato {@link IAuthRepository}.
 * @remarks Mapea los objetos de Firebase a entidades de dominio {@link Usuario}.
 */
export class AuthRepository implements IAuthRepository {
  constructor(private readonly _dataSource: FirebaseAuthDataSource) {}

  /**
   * Autentica al usuario y devuelve la entidad de dominio correspondiente.
   * @param email Correo electrónico del usuario.
   * @param password Contraseña en texto plano.
   * @returns Entidad {@link Usuario} con los datos del usuario autenticado.
   */
  async login(email: string, password: string): Promise<Usuario> {
    const credential = await this._dataSource.login(email, password);
    return this.mapFirebaseUserToUsuario(credential.user);
  }

  /** Cierra la sesión delegando en el datasource de Firebase. */
  async logout(): Promise<void> {
    await this._dataSource.logout();
  }

  /**
   * Obtiene el usuario actualmente autenticado mapeado a entidad de dominio.
   * @returns Entidad {@link Usuario} o `null` si no hay sesión activa.
   */
  async getCurrentUser(): Promise<Usuario | null> {
    const firebaseUser = this._dataSource.getCurrentUser();
    return firebaseUser ? this.mapFirebaseUserToUsuario(firebaseUser) : null;
  }

  /**
   * Comprueba si existe una sesión activa.
   * @returns `true` si hay un usuario autenticado en Firebase.
   */
  async isAuthenticated(): Promise<boolean> {
    const user = await this.getCurrentUser();
    return user !== null;
  }

  /**
   * Convierte un objeto de usuario de Firebase en una entidad {@link Usuario}.
   * @param firebaseUser Objeto de usuario de Firebase con `uid`, `email` y `displayName`.
   * @returns Nueva instancia de {@link Usuario}.
   * @internal
   */
  private mapFirebaseUserToUsuario(firebaseUser: any): Usuario {
    return new Usuario(
      firebaseUser.uid,
      firebaseUser.email || '',
      firebaseUser.displayName || firebaseUser.email?.split('@')[0] || 'Usuario',
      'user',
      true
    );
  }
}