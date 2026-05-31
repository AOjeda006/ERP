/**
 * Entidad de dominio que representa a un usuario autenticado en el sistema.
 * @remarks Contiene lógica de validación de email y generación de iniciales para la UI.
 */
export class Usuario {
  constructor(
    public id: string,
    public email: string,
    public nombre: string,
    public rol?: string,
    public activo: boolean = true
  ) {}

  /**
   * Valida si una cadena tiene formato de correo electrónico válido.
   * @param email Cadena a validar.
   * @returns `true` si el formato es válido.
   */
  static isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  /**
   * Devuelve las iniciales del nombre del usuario (máximo 2 caracteres en mayúsculas).
   * @returns Cadena con las iniciales, p. ej. `'JG'` para `'Juan García'`.
   */
  getIniciales(): string {
    return this.nombre
      .split(' ')
      .map(word => word[0])
      .join('')
      .toUpperCase()
      .substring(0, 2);
  }
}