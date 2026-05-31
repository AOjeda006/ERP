import { Auth, signInWithEmailAndPassword, signOut, UserCredential } from '@angular/fire/auth';

/**
 * Datasource de autenticación que utiliza Firebase Auth como proveedor.
 * @remarks Traduce los códigos de error de Firebase a mensajes en español antes de propagarlos.
 */
export class FirebaseAuthDataSource {
  constructor(private readonly firebaseAuth: Auth) {}

  /**
   * Autentica al usuario contra Firebase Auth.
   * @param email Correo electrónico del usuario.
   * @param password Contraseña en texto plano.
   * @returns Credencial de Firebase con el usuario autenticado.
   * @throws {Error} Con mensaje en español si Firebase devuelve un error conocido.
   */
  async login(email: string, password: string): Promise<UserCredential> {
    try {
      return await signInWithEmailAndPassword(this.firebaseAuth, email, password);
    } catch (error: any) {
      throw this.handleFirebaseError(error);
    }
  }

  /**
   * Cierra la sesión del usuario activo en Firebase Auth.
   * @throws {Error} Si Firebase no puede completar el cierre de sesión.
   */
  async logout(): Promise<void> {
    try {
      await signOut(this.firebaseAuth);
    } catch {
      throw new Error('Error al cerrar sesión');
    }
  }

  /**
   * Devuelve el usuario de Firebase actualmente autenticado o `null`.
   * @returns Objeto de usuario de Firebase (`currentUser`) o `null`.
   */
  getCurrentUser(): any {
    return this.firebaseAuth.currentUser;
  }

  /**
   * Traduce un código de error de Firebase a un mensaje en español.
   * @param error Objeto de error lanzado por Firebase con propiedad `code`.
   * @returns Instancia de {@link Error} con mensaje localizado.
   * @internal
   */
  private handleFirebaseError(error: any): Error {
    const errorMessages: { [key: string]: string } = {
      'auth/user-not-found': 'Usuario no encontrado',
      'auth/wrong-password': 'Contraseña incorrecta',
      'auth/invalid-email': 'Email inválido',
      'auth/user-disabled': 'Usuario deshabilitado',
      'auth/too-many-requests': 'Demasiados intentos. Intente más tarde',
      'auth/network-request-failed': 'Error de conexión',
      'auth/invalid-credential': 'Credenciales inválidas'
    };

    const message = errorMessages[error.code] || 'Error de autenticación';
    return new Error(message);
  }
}