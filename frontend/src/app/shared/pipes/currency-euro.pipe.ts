import { Pipe, PipeTransform } from '@angular/core';

/**
 * Pipe que formatea un número como moneda en euros con locale español.
 * @example
 * ```ts
 * {{ 1234.5 | currencyEuro }}  // → "1.234,50 €"
 * ```
 */
@Pipe({ name: 'currencyEuro', standalone: true })
export class CurrencyEuroPipe implements PipeTransform {
  /**
   * Formatea `value` como importe en euros.
   * @param value Número a formatear
   * @returns Cadena con formato EUR español
   */
  transform(value: number): string {
    return new Intl.NumberFormat('es-ES', { style: 'currency', currency: 'EUR' }).format(value);
  }
}