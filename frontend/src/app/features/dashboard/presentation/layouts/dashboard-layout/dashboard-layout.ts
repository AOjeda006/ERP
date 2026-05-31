import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { SidebarComponent } from '../../../../../shared/components/sidebar/sidebar';
import { HeaderComponent } from '../../../../../shared/components/header/header';

/**
 * Layout principal del dashboard.
 * @remarks Compone la estructura de tres zonas: cabecera ({@link HeaderComponent}),
 * barra lateral ({@link SidebarComponent}) y área de contenido principal con `<router-outlet>`.
 */
@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, HeaderComponent],
  template: `
    <div class="shell">
      <header class="header">
        <app-header></app-header>
      </header>

      <div class="body">
        <aside class="sidebar">
          <app-sidebar></app-sidebar>
        </aside>

        <main class="main">
          <router-outlet></router-outlet>
        </main>
      </div>
    </div>
  `,
  styleUrl: './dashboard-layout.scss',
})
export class DashboardLayoutComponent {}
