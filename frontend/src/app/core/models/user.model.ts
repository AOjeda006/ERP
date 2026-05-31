/** Contrato de datos para un usuario de la aplicación. */
export interface User {
  id: string;
  email: string;
  nombre: string;
  rol?: string;
  activo?: boolean;
  fechaCreacion?: Date;
}

/**
 * Modelo de dominio para un usuario autenticado.
 * @remarks Implementa {@link User} y proporciona un factory method para construirse
 * a partir del objeto de usuario de Firebase.
 */
export class UserModel implements User {
  constructor(
    public id: string,
    public email: string,
    public nombre: string,
    public rol?: string,
    public activo: boolean = true,
    public fechaCreacion?: Date
  ) {}

  /**
   * Crea un {@link UserModel} a partir de un usuario de Firebase Auth.
   * @param firebaseUser Objeto de usuario devuelto por Firebase (`uid`, `email`, `displayName`).
   * @returns Nueva instancia de {@link UserModel} con rol `'user'` y estado activo.
   */
  static fromFirebaseUser(firebaseUser: any): UserModel {
    return new UserModel(
      firebaseUser.uid,
      firebaseUser.email || '',
      firebaseUser.displayName || firebaseUser.email?.split('@')[0] || 'Usuario',
      'user',
      true,
      new Date()
    );
  }
}