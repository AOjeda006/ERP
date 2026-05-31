// Plantilla de configuración de entorno.
// Copia este archivo como `environment.ts` (desarrollo) y `environment.prod.ts`
// (producción) y rellena los valores con tu propia configuración.
export const environment = {
  production: false,

  // Desarrollo: '/api' (proxy a backend local). Producción: URL pública del backend.
  apiUrl: '/api',

  firebase: {
    apiKey: 'TU_API_KEY',
    authDomain: 'TU_PROJECT_ID.firebaseapp.com',
    projectId: 'TU_PROJECT_ID',
    storageBucket: 'TU_PROJECT_ID.appspot.com',
    messagingSenderId: 'TU_MESSAGING_SENDER_ID',
    appId: 'TU_APP_ID',
    measurementId: 'TU_MEASUREMENT_ID',
  },
};
