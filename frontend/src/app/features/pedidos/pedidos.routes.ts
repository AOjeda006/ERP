import { Routes } from '@angular/router';

/**
 * Rutas del módulo de pedidos.
 * @remarks Incluye la lista de pedidos, el formulario de creación y el de edición con parámetro `:id`.
 */
export const pedidosRoutes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./presentation/pages/pedidos-list/pedidos-list.component').then(m => m.PedidosListComponent)
  },
  {
    path: 'nuevo',
    loadComponent: () =>
      import('./presentation/pages/pedidos-form/pedidos-form').then(m => m.PedidosFormComponent)
  },
  {
    path: 'editar/:id',
    loadComponent: () =>
      import('./presentation/pages/pedidos-form/pedidos-form').then(m => m.PedidosFormComponent)
  }
];
