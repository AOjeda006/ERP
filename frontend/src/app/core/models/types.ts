/** Tipo genérico para identificadores: acepta cadena o número. */
export type ID = string | number;

/**
 * Envoltorio genérico para respuestas de la API REST.
 * @template T Tipo del payload de datos devuelto.
 */
export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message?: string;
}

/**
 * Respuesta paginada genérica de la API REST.
 * @template T Tipo de cada elemento de la página.
 */
export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
}

/** Estructura estándar de error devuelta por la API REST. */
export interface ErrorResponse {
  error: string;
  code?: string;
  details?: any;
}