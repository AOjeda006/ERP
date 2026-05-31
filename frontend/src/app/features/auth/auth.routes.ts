import { Routes } from '@angular/router';
import { noAuthGuard } from '../../core/guards/no-auth.guard';

/**
 * Rutas del módulo de autenticación.
 * @remarks La ruta `/login` está protegida por {@link noAuthGuard} para redirigir
 * al dashboard si el usuario ya tiene sesión activa.
 */
export const authRoutes: Routes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./presentation/pages/login/login.component').then(m => m.LoginComponent),
    canActivate: [noAuthGuard]
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];