import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

/**
 * Validador de formato de email.
 * @returns `null` si el email es válido, `{ invalidEmail }` en caso contrario
 */
export function emailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(control.value) ? null : { invalidEmail: { value: control.value } };
  };
}

/**
 * Validador de fecha parseable por `new Date()`.
 * @returns `null` si la fecha es válida, `{ invalidDate }` en caso contrario
 */
export function dateValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valid = !isNaN(new Date(control.value).getTime());
    return valid ? null : { invalidDate: { value: control.value } };
  };
}