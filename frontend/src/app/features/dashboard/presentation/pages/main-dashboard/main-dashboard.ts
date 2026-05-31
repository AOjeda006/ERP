import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../../../../../../environments/environment';

/**
 * Pantalla principal del dashboard con KPIs de pedidos, productos y proveedores.
 * @remarks Usa llamadas directas a HttpClient (no Container) para evitar
 * confusión con las instancias duplicadas de los ViewModels.
 */
@Component({
  selector: 'app-main-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './main-dashboard.html',
  styleUrl: './main-dashboard.scss'
})
export class MainDashboardComponent implements OnInit {
  private readonly http = inject(HttpClient);

  kpiPedidos$:     Observable<number> = of(0);
  kpiProductos$:   Observable<number> = of(0);
  kpiProveedores$: Observable<number> = of(0);

  /** Inicializa los observables de KPI realizando peticiones al backend. */
  ngOnInit(): void {
    this.kpiPedidos$ = this.http.get<any[]>(`${environment.apiUrl}/Pedidos`).pipe(
      map(x => (x ?? []).length), catchError(() => of(0))
    );
    this.kpiProductos$ = this.http.get<any[]>(`${environment.apiUrl}/Productos`).pipe(
      map(x => (x ?? []).length), catchError(() => of(0))
    );
    this.kpiProveedores$ = this.http.get<any[]>(`${environment.apiUrl}/Proveedores`).pipe(
      map(x => (x ?? []).length), catchError(() => of(0))
    );
  }
}
