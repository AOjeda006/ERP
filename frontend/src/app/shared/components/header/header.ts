import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { User } from '../../../core/models/user.model';

/**
 * Barra superior de la aplicación con logo, avatar del usuario y botón de logout.
 */
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, MatToolbarModule, MatButtonModule, MatIconModule],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class HeaderComponent {
  private readonly auth   = inject(AuthService);
  private readonly router = inject(Router);

  user$: Observable<User | null> = this.auth.getCurrentUser();

  /** Cierra la sesión del usuario y redirige al login. */
  async logout(): Promise<void> {
    await this.auth.logout();
    this.router.navigate(['/login']);
  }

  /** Navega a la página de login (usado en el header cuando no hay sesión). */
  navigateToLogin(): void {
    this.router.navigate(['/login']);
  }
}