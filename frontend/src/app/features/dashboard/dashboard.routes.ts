import { Routes } from '@angular/router';
import { authGuard } from '../../core/guards/auth.guard';

/**
 * Rutas del módulo dashboard.
 * @remarks Protegidas por {@link authGuard}. Incluye pedidos, productos, proveedores,
 * almacén y la pantalla en construcción para reportes.
 */
export const dashboardRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./presentation/layouts/dashboard-layout/dashboard-layout').then(m => m.DashboardLayoutComponent),
    canActivate: [authGuard],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./presentation/pages/main-dashboard/main-dashboard').then(m => m.MainDashboardComponent)
      },
      {
        path: 'pedidos',
        loadChildren: () =>
          import('../pedidos/pedidos.routes').then(m => m.pedidosRoutes)
      },
      {
        path: 'productos',
        loadComponent: () =>
          import('../productos/presentation/pages/productos/productos.component').then(m => m.ProductosComponent)
      },
      {
        path: 'proveedores',
        loadComponent: () =>
          import('../proveedores/presentation/pages/proveedores/proveedores.component').then(m => m.ProveedoresComponent)
      },
      {
        path: 'almacen',
        loadComponent: () =>
          import('../almacen/presentation/pages/almacen/almacen.component').then(m => m.AlmacenComponent)
      },
      {
        path: 'reportes',
        loadComponent: () =>
          import('./presentation/pages/en-construccion/en-construccion').then(m => m.EnConstruccionComponent)
      }
    ]
  }
];
