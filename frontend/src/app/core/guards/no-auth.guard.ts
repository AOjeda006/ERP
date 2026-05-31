import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, tap } from 'rxjs/operators';

/**
 * Guard que impide acceder al login si ya hay sesión activa.
 * @remarks Redirige a `/dashboard` si el usuario ya está autenticado.
 */
export const noAuthGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router      = inject(Router);

  return authService.isAuthenticated().pipe(
    tap(isAuth => { if (isAuth) router.navigate(['/dashboard']); }),
    map(isAuth => !isAuth)
  );
};
