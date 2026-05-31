export const environment = {
  production: false,

  // En desarrollo las peticiones a /api se redirigen al backend local
  // mediante el proxy (src/proxy.conf.json).
  apiUrl: '/api',

  // Configuración del proyecto Firebase (Authentication).
  // Rellena estos valores con los de tu propio proyecto Firebase.
  // Consola: https://console.firebase.google.com → Configuración del proyecto.
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
