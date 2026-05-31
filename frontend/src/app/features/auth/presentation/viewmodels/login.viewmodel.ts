import { BehaviorSubject } from 'rxjs';
import { ILoginUseCase } from '../../domain/interfaces/ILoginUseCase';

/**
 * ViewModel del formulario de login.
 * @remarks Gestiona el estado reactivo de email, contraseña, carga y errores.
 * @see {@link LoginComponent}
 */
export class LoginViewModel {
  public email        = new BehaviorSubject<string>('');
  public password     = new BehaviorSubject<string>('');
  public isLoading    = new BehaviorSubject<boolean>(false);
  public errorMessage = new BehaviorSubject<string | null>(null);

  public email$        = this.email.asObservable();
  public password$     = this.password.asObservable();
  public isLoading$    = this.isLoading.asObservable();
  public errorMessage$ = this.errorMessage.asObservable();

  constructor(private readonly _loginUseCase: ILoginUseCase) {}

  /**
   * Actualiza el email y limpia el error previo.
   * @param email Valor introducido en el campo email
   */
  setEmail(email: string): void {
    this.email.next(email);
    this.clearError();
  }

  /**
   * Actualiza la contraseña y limpia el error previo.
   * @param password Valor introducido en el campo contraseña
   */
  setPassword(password: string): void {
    this.password.next(password);
    this.clearError();
  }

  /**
   * Ejecuta el caso de uso de login con las credenciales actuales.
   * @remarks Emite errores mediante {@link errorMessage$} sin lanzar excepciones.
   */
  async login(): Promise<void> {
    const email    = this.email.value;
    const password = this.password.value;

    if (!email || !password) {
      this.errorMessage.next('Por favor complete todos los campos');
      return;
    }

    this.isLoading.next(true);
    this.errorMessage.next(null);
    try {
      await this._loginUseCase.execute(email, password);
    } catch (error: any) {
      this.errorMessage.next(error.message || 'Error al iniciar sesión');
    } finally {
      this.isLoading.next(false);
    }
  }

  /** Limpia el mensaje de error si existe. */
  clearError(): void {
    if (this.errorMessage.value) this.errorMessage.next(null);
  }

  /** Restablece todos los campos del formulario. */
  clearForm(): void {
    this.email.next('');
    this.password.next('');
    this.errorMessage.next(null);
  }
}
