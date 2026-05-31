import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { Container, TYPES } from '../../../../../core/container/container';
import { ReportesViewModel } from '../../viewmodels/reportes.viewmodel';

/**
 * Pantalla de reportes del ERP.
 * @remarks Obtiene el {@link ReportesViewModel} del contenedor DI y lo expone a la plantilla.
 * @see {@link ReportesViewModel}
 */
@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './reportes.component.html',
  styleUrl: './reportes.component.scss'
})
export class ReportesComponent implements OnInit {
  vm!: ReportesViewModel;

  /** Resuelve el ViewModel desde el contenedor DI en la inicialización. */
  ngOnInit(): void {
    this.vm = Container.getInstance().resolve<ReportesViewModel>(TYPES.ReportesViewModel);
  }
}
