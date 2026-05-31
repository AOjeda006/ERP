import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';

/**
 * Pantalla de marcador de posición para secciones pendientes de implementar.
 * @remarks Se usa como ruta de destino para módulos como Reportes mientras no están disponibles.
 */
@Component({
  selector: 'app-en-construccion',
  standalone: true,
  imports: [MatIconModule, MatCardModule, MatButtonModule, RouterLink],
  templateUrl: './en-construccion.html',
  styleUrls: ['./en-construccion.scss']
})
export class EnConstruccionComponent {}