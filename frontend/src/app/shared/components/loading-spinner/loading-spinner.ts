import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingService } from '../../../core/services/loading.service';
import { Observable } from 'rxjs';

/**
 * Overlay de spinner global que se muestra mientras hay peticiones HTTP en curso.
 * @remarks Recibe el estado de {@link LoadingService} y lo refleja en la vista.
 */
@Component({
  selector: 'app-loading-spinner',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading-spinner.html',
  styleUrls: ['./loading-spinner.scss']
})
export class LoadingSpinnerComponent implements OnInit {
  private loadingService = inject(LoadingService);

  isLoading$!: Observable<boolean>;

  /** Enlaza {@link isLoading$} al observable de estado del {@link LoadingService}. */
  ngOnInit(): void {
    this.isLoading$ = this.loadingService.loading$;
  }
}