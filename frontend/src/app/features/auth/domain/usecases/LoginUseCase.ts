import { ILoginUseCase } from '../interfaces/ILoginUseCase';
import { IAuthRepository } from '../interfaces/IAuthRepository';
import { Usuario } from '../entities/usuario.entity';

/**
 * Caso de uso de inicio de sesión.
 * @remarks Valida el formato del email y la longitud mínima de la contraseña antes de
 * delegar en {@link IAuthRepository}. Implementa {@link ILoginUseCase}.
 */
export class LoginUseCase implements ILoginUseCase {
  constructor(private readonly _authRepository: IAuthRepository) {}

  /**
   * Valida las credenciales y delega la autenticación al repositorio.
   * @param email Correo electrónico del usuario.
   * @param password Contraseña en texto plano (mínimo 6 caracteres).
   * @throws {Error} Si el email no tiene formato válido o la contraseña es demasiado corta.
   * @throws {Error} Si el repositorio rechaza las credenciales.
   */
  async execute(email: string, password: string): Promise<void> {
    if (!Usuario.isValidEmail(email)) {
      throw new Error('Email inválido');
    }

    if (!password || password.length < 6) {
      throw new Error('La contraseña debe tener al menos 6 caracteres');
    }

    await this._authRepository.login(email, password);
  }
}