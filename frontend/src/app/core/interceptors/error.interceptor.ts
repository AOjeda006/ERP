import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification.service';

/**
 * Mapea un código HTTP a un mensaje de error localizado.
 * @param status Código de estado HTTP
 * @returns Mensaje en español o cadena vacía si no hay mapeo
 * @internal
 */
const getErrorMessageByStatus = (status: number): string => {
  const errorMap: Record<number, string> = {
    400: 'Petición inválida',
    401: 'No autorizado. Por favor, inicie sesión',
    403: 'No tiene permisos para realizar esta acción',
    404: 'Recurso no encontrado',
    500: 'Error interno del servidor',
    503: 'Servicio no disponible'
  };
  return errorMap[status] || '';
};

/**
 * Interceptor HTTP que muestra un toast de error y redirige a login en 401.
 * @remarks Se registra en {@link appConfig} junto a los demás interceptores.
 */
export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router              = inject(Router);
  const notificationService = inject(NotificationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const isClientError    = error.error instanceof ErrorEvent;
      const clientMessage    = isClientError ? `Error: ${error.error.message}` : '';
      const statusMessage    = getErrorMessageByStatus(error.status);
      const fallbackMessage  = (error.error as any)?.message || `Error ${error.status}: ${error.statusText}`;
      const errorMessage     = isClientError ? clientMessage : (statusMessage || fallbackMessage);

      notificationService.showError(errorMessage);
      if (error.status === 401) router.navigate(['/auth/login']);

      return throwError(() => error);
    })
  );
};