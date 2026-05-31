import { inject } from '@angular/core';
import { Router, CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, tap } from 'rxjs/operators';

/**
 * Guard que protege las rutas privadas del dashboard.
 * @remarks Redirige a `/login` con `returnUrl` si el usuario no está autenticado.
 */
export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService = inject(AuthService);
  const router      = inject(Router);

  return authService.isAuthenticated().pipe(
    tap(isAuth => {
      if (!isAuth) router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    }),
    map(isAuth => isAuth)
  );
};
