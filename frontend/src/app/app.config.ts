import { APP_INITIALIZER, ApplicationConfig, Injector, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';

import { provideFirebaseApp, initializeApp } from '@angular/fire/app';
import { provideAuth, getAuth } from '@angular/fire/auth';
import { provideFirestore, getFirestore } from '@angular/fire/firestore';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth.interceptor';
import { errorInterceptor } from './core/interceptors/error.interceptor';
import { loadingInterceptor } from './core/interceptors/loading.interceptor';
import { setupDependencyInjection } from './core/container/di-setup';
import { environment } from '../environments/environment';

/** Configuración del proyecto Firebase para autenticación y Firestore. */
const firebaseConfig = environment.firebase;

/**
 * Configuración principal de la aplicación Angular.
 * @remarks Registra HttpClient con interceptores, animaciones, Firebase y el contenedor DI personalizado
 * mediante un {@link APP_INITIALIZER} que ejecuta {@link setupDependencyInjection} al arrancar.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        authInterceptor,
        errorInterceptor,
        loadingInterceptor
      ])
    ),
    provideAnimations(),
    provideFirebaseApp(() => initializeApp(firebaseConfig)),
    provideAuth(() => getAuth()),
    provideFirestore(() => getFirestore()),
    {
      provide: APP_INITIALIZER,
      multi: true,
      deps: [Injector],
      useFactory: (injector: Injector) => () => {
        setupDependencyInjection(injector);
      }
    }
  ]
};
