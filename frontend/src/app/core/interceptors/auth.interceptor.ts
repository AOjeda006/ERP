import { inject } from '@angular/core';
import {
  HttpEvent,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Auth } from '@angular/fire/auth';
import { from, Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

/**
 * Interceptor HTTP que adjunta el Bearer token de Firebase a cada petición.
 * @remarks Si no hay sesión activa la petición se envía sin cabecera Authorization.
 */
export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const auth = inject(Auth);
  const tokenPromise = auth.currentUser?.getIdToken() ?? Promise.resolve('');

  return from(tokenPromise).pipe(
    switchMap((token: string) => {
      if (!token) return next(req);
      return next(req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }));
    })
  );
};
