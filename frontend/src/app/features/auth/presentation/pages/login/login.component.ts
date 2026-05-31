import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
/**
 * Pantalla de inicio de sesión.
 * @remarks Gestiona el estado del formulario directamente (sin ViewModel separado).
 * Tras autenticarse redirige a `/dashboard`; los errores se muestran en la misma vista.
 */
export class LoginComponent {
  private readonly auth = inject(AuthService);
  private readonly router = inject(Router);

  email: string = '';
  password: string = '';

  isLoading = false;
  errorMessage: string | null = null;

  /**
   * Maneja el envío del formulario de login.
   * @remarks Valida que los campos no estén vacíos antes de llamar a {@link AuthService.login}.
   */
  async onSubmit(): Promise<void> {
    this.errorMessage = null;

    if (!this.email || !this.password) {
      this.errorMessage = 'Email y contraseña son obligatorios.';
      return;
    }

    try {
      this.isLoading = true;
      await this.auth.login(this.email, this.password);
      this.router.navigate(['/dashboard']);
    } catch (err: any) {
      this.errorMessage = err?.message ?? 'Error al iniciar sesión';
    } finally {
      this.isLoading = false;
    }
  }
}
