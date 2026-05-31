import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

/**
 * Gestiona el estado global del spinner de carga.
 * @remarks Cuenta peticiones activas para no ocultar el spinner
 * mientras haya otras peticiones en curso.
 */
@Injectable({ providedIn: 'root' })
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private activeRequests = 0;

  public loading$: Observable<boolean> = this.loadingSubject.asObservable();

  /** Incrementa el contador de peticiones activas y muestra el spinner. */
  show(): void {
    this.activeRequests++;
    if (this.activeRequests > 0) this.loadingSubject.next(true);
  }

  /** Decrementa el contador; oculta el spinner cuando llega a cero. */
  hide(): void {
    this.activeRequests--;
    if (this.activeRequests <= 0) {
      this.activeRequests = 0;
      this.loadingSubject.next(false);
    }
  }

  /** Oculta el spinner inmediatamente, ignorando peticiones pendientes. */
  forceHide(): void {
    this.activeRequests = 0;
    this.loadingSubject.next(false);
  }

  /** Devuelve `true` si hay alguna petición en curso. */
  isLoading(): boolean {
    return this.loadingSubject.value;
  }
}