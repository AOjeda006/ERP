import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

/**
 * Tabla de rutas raíz de la aplicación.
 * @remarks La ruta vacía redirige a `/login`. El módulo `dashboard` está protegido
 * por {@link authGuard} y carga todos los módulos hijos de forma lazy.
 * Cualquier ruta desconocida también redirige a `/login`.
 */
export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },

  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/presentation/pages/login/login.component')
        .then(m => m.LoginComponent),
  },

  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/dashboard/presentation/layouts/dashboard-layout/dashboard-layout')
        .then(m => m.DashboardLayoutComponent),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/dashboard/presentation/pages/main-dashboard/main-dashboard')
            .then(m => m.MainDashboardComponent),
      },
      {
        path: 'pedidos',
        loadComponent: () =>
          import('./features/pedidos/presentation/pages/pedidos-list/pedidos-list.component')
            .then(m => m.PedidosListComponent),
      },
      {
        path: 'pedidos/nuevo',
        loadComponent: () =>
          import('./features/pedidos/presentation/pages/pedidos-form/pedidos-form')
            .then(m => m.PedidosFormComponent),
      },
      {
        path: 'pedidos/editar/:id',
        loadComponent: () =>
          import('./features/pedidos/presentation/pages/pedidos-form/pedidos-form')
            .then(m => m.PedidosFormComponent),
      },
      {
        path: 'productos',
        loadComponent: () =>
          import('./features/productos/presentation/pages/productos/productos.component')
            .then(m => m.ProductosComponent),
      },
      {
        path: 'proveedores',
        loadComponent: () =>
          import('./features/proveedores/presentation/pages/proveedores/proveedores.component')
            .then(m => m.ProveedoresComponent),
      },
      {
        path: 'almacen',
        loadComponent: () =>
          import('./features/almacen/presentation/pages/almacen/almacen.component')
            .then(m => m.AlmacenComponent),
      },
      {
        path: 'reportes',
        loadComponent: () =>
          import('./features/reportes/presentation/pages/reportes/reportes.component')
            .then(m => m.ReportesComponent),
      },
    ],
  },

  { path: '**', redirectTo: 'login' },
];