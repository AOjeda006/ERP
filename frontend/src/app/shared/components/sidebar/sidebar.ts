import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

/**
 * Barra de navegación lateral con los ítems del menú principal.
 * @remarks En pantallas <900 px se convierte en barra horizontal.
 */
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrls: ['./sidebar.scss'],
})
export class SidebarComponent {
  /** Definición estática de los ítems del menú. */
  menuItems = [
    { label: 'Dashboard',   route: '/dashboard',            icon: '📊', linkOptions: { exact: true } },
    { label: 'Pedidos',     route: '/dashboard/pedidos',    icon: '📦', linkOptions: { exact: false } },
    { label: 'Almacén',     route: '/dashboard/almacen',    icon: '🏭', linkOptions: { exact: false } },
    { label: 'Productos',   route: '/dashboard/productos',  icon: '🏷️', linkOptions: { exact: false } },
    { label: 'Proveedores', route: '/dashboard/proveedores',icon: '🏢', linkOptions: { exact: false } },
  ];
}
