import { Injectable, inject } from '@angular/core';
import { Auth, signInWithEmailAndPassword, signOut, onAuthStateChanged, User as FirebaseUser } from '@angular/fire/auth';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User, UserModel } from '../models/user.model';

/**
 * Servicio de autenticación basado en Firebase Auth.
 * @remarks Expone el usuario actual como observable reactivo.
 * Suscribirse a {@link currentUser$} para reaccionar a cambios de sesión.
 */
@Injectable({ providedIn: 'root' })
export class AuthService {
  private firebaseAuth       = inject(Auth);
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public  currentUser$       = this.currentUserSubject.asObservable();

  constructor() {
    onAuthStateChanged(this.firebaseAuth, (firebaseUser: FirebaseUser | null) => {
      if (firebaseUser) {
        const user = UserModel.fromFirebaseUser(firebaseUser);
        this.currentUserSubject.next(user);
      } else {
        this.currentUserSubject.next(null);
      }
    });
  }

  /**
   * Inicia sesión con email y contraseña.
   * @param email Email del usuario
   * @param password Contraseña en texto plano
   * @throws Error con mensaje localizado si las credenciales son inválidas
   */
  async login(email: string, password: string): Promise<void> {
    try {
      const credential = await signInWithEmailAndPassword(this.firebaseAuth, email, password);
      this.currentUserSubject.next(UserModel.fromFirebaseUser(credential.user));
    } catch (error: any) {
      throw new Error(this.getErrorMessage(error.code));
    }
  }

  /**
   * Cierra la sesión del usuario actual.
   * @throws Error si Firebase no puede completar el logout
   */
  async logout(): Promise<void> {
    await signOut(this.firebaseAuth);
    this.currentUserSubject.next(null);
  }

  /** Devuelve el usuario actual como Observable. */
  getCurrentUser(): Observable<User | null> {
    return this.currentUser$;
  }

  /** Emite `true` cuando hay una sesión activa. */
  isAuthenticated(): Observable<boolean> {
    return this.currentUser$.pipe(map(user => user !== null));
  }

  /** Devuelve el usuario actual de forma síncrona (puede ser `null`). */
  getCurrentUserSync(): User | null {
    return this.currentUserSubject.value;
  }

  /**
   * Traduce un código de error Firebase a un mensaje legible en español.
   * @param errorCode Código de error de Firebase (ej. `auth/wrong-password`)
   * @internal
   */
  private getErrorMessage(errorCode: string): string {
    const errors: { [key: string]: string } = {
      'auth/user-not-found':         'Usuario no encontrado',
      'auth/wrong-password':         'Contraseña incorrecta',
      'auth/invalid-email':          'Email inválido',
      'auth/user-disabled':          'Usuario deshabilitado',
      'auth/too-many-requests':      'Demasiados intentos. Intente más tarde',
      'auth/network-request-failed': 'Error de conexión',
      'auth/invalid-credential':     'Credenciales inválidas'
    };
    return errors[errorCode] || 'Error de autenticación';
  }
}