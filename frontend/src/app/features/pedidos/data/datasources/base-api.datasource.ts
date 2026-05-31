import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { joinUrl } from '../../../../core/http/url';

/**
 * Clase base abstracta para todos los datasources de la API REST.
 * @remarks Proporciona métodos HTTP tipados (`get`, `post`, `put`, `del`, `patch`)
 * que envuelven `HttpClient` y convierten los Observables en Promises mediante `firstValueFrom`.
 * La URL base se obtiene de {@link environment.apiUrl}.
 */
export abstract class BaseApiDataSource {
  protected readonly baseUrl = environment.apiUrl;

  constructor(protected readonly http: HttpClient) {}

  /**
   * Realiza una petición GET.
   * @param path Segmento de ruta relativo a la URL base (p. ej. `'Pedidos'`).
   * @returns Promesa con el cuerpo de la respuesta tipado como `T`.
   */
  protected get<T>(path: string): Promise<T> {
    return firstValueFrom(this.http.get<T>(joinUrl(this.baseUrl, path)));
  }

  /**
   * Realiza una petición POST.
   * @param path Segmento de ruta relativo a la URL base.
   * @param body Payload del cuerpo de la petición.
   * @returns Promesa con el cuerpo de la respuesta tipado como `T`.
   */
  protected post<T>(path: string, body: any): Promise<T> {
    return firstValueFrom(this.http.post<T>(joinUrl(this.baseUrl, path), body));
  }

  /**
   * Realiza una petición PUT.
   * @param path Segmento de ruta relativo a la URL base.
   * @param body Payload del cuerpo de la petición.
   * @returns Promesa con el cuerpo de la respuesta tipado como `T`.
   */
  protected put<T>(path: string, body: any): Promise<T> {
    return firstValueFrom(this.http.put<T>(joinUrl(this.baseUrl, path), body));
  }

  /**
   * Realiza una petición DELETE.
   * @param path Segmento de ruta relativo a la URL base.
   * @returns Promesa con el cuerpo de la respuesta tipado como `T`.
   */
  protected del<T>(path: string): Promise<T> {
    return firstValueFrom(this.http.delete<T>(joinUrl(this.baseUrl, path)));
  }

  /**
   * Realiza una petición PATCH.
   * @param path Segmento de ruta relativo a la URL base.
   * @param body Payload parcial del cuerpo de la petición.
   * @returns Promesa con el cuerpo de la respuesta tipado como `T`.
   */
  protected patch<T>(path: string, body: any): Promise<T> {
    return firstValueFrom(this.http.patch<T>(joinUrl(this.baseUrl, path), body));
  }
}
