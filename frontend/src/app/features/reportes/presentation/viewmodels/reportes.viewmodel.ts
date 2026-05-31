import { BehaviorSubject, combineLatest, from, map } from 'rxjs';

import type { IPedidoUseCases } from '../../../pedidos/domain/interfaces/usecases/IPedidoUseCases';
import type { IProductoUseCases } from '../../../pedidos/domain/interfaces/usecases/IProductoUseCases';
import type { IProveedorUseCases } from '../../../pedidos/domain/interfaces/usecases/IProveedorUseCases';
import type { Pedido } from '../../../pedidos/domain/entities/pedido';
import type { Producto } from '../../../pedidos/domain/entities/producto';
import type { Proveedor } from '../../../pedidos/domain/entities/proveedor';

/** KPIs agregados que muestra la pantalla de reportes. */
export type ReportesKpis = {
  totalPedidos: number;
  totalImporte: number;
  productosActivos: number;
  proveedoresActivos: number;
};

/**
 * ViewModel de reportes: agrega KPIs a partir de los UseCases de dominio.
 * @remarks Los UseCases devuelven `Promise<T[]>`, convertidos a Observable con `from()`.
 */
export class ReportesViewModel {
  private isLoadingSubject = new BehaviorSubject<boolean>(false);
  isLoading$ = this.isLoadingSubject.asObservable();

  /**
   * Observable con los KPIs calculados al combinar pedidos, productos y proveedores.
   * @remarks `productosActivos` y `proveedoresActivos` son totales (sin campo `activo` en backend).
   */
  kpis$ = combineLatest([
    from(this._pedidoUseCases.getAllAsync()),
    from(this._productoUseCases.getAllAsync()),
    from(this._proveedorUseCases.getAllAsync())
  ] as const).pipe(
    map(([pedidos, productos, proveedores]): ReportesKpis => {
      const totalImporte = (pedidos as Pedido[]).reduce(
        (sum: number, p: Pedido) => sum + (p.importeTotalConIVA ?? 0), 0
      );
      return {
        totalPedidos:       (pedidos    as Pedido[]).length,
        totalImporte,
        productosActivos:   (productos  as Producto[]).length,
        proveedoresActivos: (proveedores as Proveedor[]).length
      };
    })
  );

  constructor(
    private readonly _pedidoUseCases:    IPedidoUseCases,
    private readonly _productoUseCases:  IProductoUseCases,
    private readonly _proveedorUseCases: IProveedorUseCases
  ) {}
}
