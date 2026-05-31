/**
 * Combina una URL base y un segmento de ruta normalizando las barras intermedias.
 * @param base URL base (p. ej. `https://api.example.com/v1/`).
 * @param path Segmento a añadir (p. ej. `/Pedidos`).
 * @returns URL completa sin barras dobles en el punto de unión.
 * @example
 * ```ts
 * joinUrl('https://api.example.com/', '/Pedidos') // 'https://api.example.com/Pedidos'
 * ```
 */
export function joinUrl(base: string, path: string): string {
  const b = base.endsWith('/') ? base.slice(0, -1) : base;
  const p = path.startsWith('/') ? path.slice(1) : path;
  return `${b}/${p}`;
}
