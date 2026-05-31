import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

/**
 * Componente genérico de confirmación basado en @Input/@Output.
 * @remarks Alternativa ligera a MatDialog para confirmaciones simples.
 * @see {@link PedidoConfirmDeleteDialogComponent} para el patrón Material Dialog.
 */
@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirm-dialog.html'
})
export class ConfirmDialogComponent {
  /** Título del diálogo. Por defecto `'¿Está seguro?'`. */
  @Input() title: string = '¿Está seguro?';
  /** Cuerpo del mensaje de confirmación. */
  @Input() message: string = 'Esta acción no se puede deshacer';
  /** Texto del botón de confirmación. Por defecto `'Confirmar'`. */
  @Input() confirmText: string = 'Confirmar';
  /** Texto del botón de cancelación. Por defecto `'Cancelar'`. */
  @Input() cancelText: string = 'Cancelar';
  /** Controla la visibilidad del diálogo. */
  @Input() isVisible: boolean = false;

  /** Se emite cuando el usuario pulsa el botón de confirmación. */
  @Output() onConfirm = new EventEmitter<void>();
  /** Se emite cuando el usuario pulsa el botón de cancelación. */
  @Output() onCancel  = new EventEmitter<void>();

  /** Emite el evento {@link onConfirm} cuando el usuario acepta. */
  handleConfirm(): void {
    this.onConfirm.emit();
  }

  /** Emite el evento {@link onCancel} cuando el usuario cancela. */
  handleCancel(): void {
    this.onCancel.emit();
  }
}