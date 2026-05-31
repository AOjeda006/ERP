/**
 * Contrato del caso de uso de inicio de sesión.
 * @remarks Implementado por {@link LoginUseCase}.
 */
export interface ILoginUseCase {
  /**
   * Ejecuta el flujo de autenticación con las credenciales proporcionadas.
   * @param email Correo electrónico del usuario.
   * @param password Contraseña en texto plano.
   * @throws {Error} Si el email es inválido, la contraseña es demasiado corta o las credenciales son incorrectas.
   */
  execute(email: string, password: string): Promise<void>;
}