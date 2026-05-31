import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

/**
 * Servicio centralizado de notificaciones de usuario.
 * @remarks Muestra toasts no bloqueantes con Angular Material Snackbar.
 * Los estilos de color se definen en `styles.scss` mediante panelClass.
 */
@Injectable({ providedIn: 'root' })
export class NotificationService {
  private readonly snackBar = inject(MatSnackBar);

  /**
   * Muestra una notificación de éxito (verde, 3 s).
   * @param message Texto a mostrar
   */
  showSuccess(message: string): void {
    this.snackBar.open(message, undefined, {
      duration: 3000,
      panelClass: ['snack-success'],
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
    });
  }

  /**
   * Muestra una notificación de error (rojo, 5 s con botón de cierre).
   * @param message Texto a mostrar
   */
  showError(message: string): void {
    this.snackBar.open(message, '✕', {
      duration: 5000,
      panelClass: ['snack-error'],
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
    });
  }

  /**
   * Muestra una notificación informativa (índigo, 3 s).
   * @param message Texto a mostrar
   */
  showInfo(message: string): void {
    this.snackBar.open(message, undefined, {
      duration: 3000,
      panelClass: ['snack-info'],
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
    });
  }

  /**
   * Muestra una notificación de advertencia (ámbar, 4 s).
   * @param message Texto a mostrar
   */
  showWarning(message: string): void {
    this.snackBar.open(message, undefined, {
      duration: 4000,
      panelClass: ['snack-warning'],
      horizontalPosition: 'right',
      verticalPosition: 'bottom',
    });
  }
}
