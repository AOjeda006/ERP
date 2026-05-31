import { Directive, ElementRef, AfterViewInit } from '@angular/core';

/**
 * Directiva que enfoca el elemento host al renderizarse.
 * @example
 * ```html
 * <input appAutoFocus />
 * ```
 */
@Directive({ selector: '[appAutoFocus]', standalone: true })
export class AutoFocusDirective implements AfterViewInit {
  constructor(private el: ElementRef) {}

  /** Enfoca el elemento nativo en cuanto el componente termina de renderizarse. */
  ngAfterViewInit(): void {
    this.el.nativeElement.focus();
  }
}