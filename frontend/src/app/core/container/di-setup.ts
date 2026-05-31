import { Injector, runInInjectionContext } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Auth } from '@angular/fire/auth';

import { Container, TYPES } from './container';

import { FirebaseAuthDataSource } from '../../features/auth/data/datasources/FirebaseAuthDataSource';
import { AuthRepository } from '../../features/auth/data/repositories/AuthRepository';
import { LoginUseCase } from '../../features/auth/domain/usecases/LoginUseCase';
import { LoginViewModel } from '../../features/auth/presentation/viewmodels/login.viewmodel';

import { CategoriaApiDataSource } from '../../features/pedidos/data/datasources/categoria-api.datasource';
import { DetallePedidoApiDataSource } from '../../features/pedidos/data/datasources/detalle-pedido-api.datasource';
import { EstadoPedidoApiDataSource } from '../../features/pedidos/data/datasources/estado-pedido-api.datasource';
import { PedidoApiDataSource } from '../../features/pedidos/data/datasources/pedido-api.datasource';
import { ProductoApiDataSource } from '../../features/pedidos/data/datasources/producto-api.datasource';
import { ProveedorApiDataSource } from '../../features/pedidos/data/datasources/proveeedor-api.datasource';
import { ProductoProveedorApiDataSource } from '../../features/pedidos/data/datasources/producto-proveedor-api.datasource';

import { CategoriaRepository } from '../../features/pedidos/data/repositories/CategoriaRepository';
import { DetallePedidoRepository } from '../../features/pedidos/data/repositories/DetallePedidoRepository';
import { EstadoPedidoRepository } from '../../features/pedidos/data/repositories/EstadoPedidoRepository';
import { PedidoRepository } from '../../features/pedidos/data/repositories/PedidoRepository';
import { ProductoRepository } from '../../features/pedidos/data/repositories/ProductoRepository';
import { ProveedorRepository } from '../../features/pedidos/data/repositories/ProveedorRepository';

import { CategoriaUseCases } from '../../features/pedidos/domain/usecases/CategoriaUseCases';
import { DetallePedidoUseCases } from '../../features/pedidos/domain/usecases/DetallePedidoUseCases';
import { EstadoPedidoUseCases } from '../../features/pedidos/domain/usecases/EstadoPedidoUseCases';
import { PedidoUseCases } from '../../features/pedidos/domain/usecases/PedidoUseCases';
import { ProductoUseCases } from '../../features/pedidos/domain/usecases/ProductoUseCases';
import { ProveedorUseCases } from '../../features/pedidos/domain/usecases/ProveedorUseCases';

import { PedidosViewModel } from '../../features/pedidos/presentation/viewmodels/pedidos.viewmodels';
import { ProductoViewModel } from '../../features/productos/presentation/viewmodels/producto.viewmodel';
import { ProveedoresViewModel } from '../../features/proveedores/presentation/viewmodels/proveedores.viewmodel';
import { ReportesViewModel } from '../../features/reportes/presentation/viewmodels/reportes.viewmodel';

/**
 * Configura el {@link Container} manual de inyección de dependencias.
 * Se ejecuta como `APP_INITIALIZER` antes de que arranque la aplicación.
 * @remarks Protegido con `container.has()` para evitar doble registro en HMR.
 * @param injector Inyector de Angular para obtener `HttpClient` y `Auth`
 */
export function setupDependencyInjection(injector: Injector): void {
  const container = Container.getInstance();

  if (container.has(TYPES.LoginViewModel)) return;  // evita doble registro en dev/HMR

  runInInjectionContext(injector, () => {
    const http = injector.get(HttpClient);
    const auth = injector.get(Auth);

    const firebaseAuthDataSource = new FirebaseAuthDataSource(auth);
    container.register(TYPES.FirebaseAuthDataSource, firebaseAuthDataSource);
    const authRepository = new AuthRepository(firebaseAuthDataSource);
    container.register(TYPES.IAuthRepository, authRepository);
    const loginUseCase = new LoginUseCase(authRepository);
    container.register(TYPES.ILoginUseCase, loginUseCase);
    const loginViewModel = new LoginViewModel(loginUseCase);
    container.register(TYPES.LoginViewModel, loginViewModel);

    const categoriaDs       = new CategoriaApiDataSource(http);
    const detallePedidoDs   = new DetallePedidoApiDataSource(http);
    const estadoPedidoDs    = new EstadoPedidoApiDataSource(http);
    const pedidoDs          = new PedidoApiDataSource(http);
    const productoDs        = new ProductoApiDataSource(http);
    const proveedorDs       = new ProveedorApiDataSource(http);
    const productoPoveedorDs = new ProductoProveedorApiDataSource(http);

    container.register(TYPES.CategoriaApiDataSource,      categoriaDs);
    container.register(TYPES.DetallePedidoApiDataSource,  detallePedidoDs);
    container.register(TYPES.EstadoPedidoApiDataSource,   estadoPedidoDs);
    container.register(TYPES.PedidoApiDataSource,         pedidoDs);
    container.register(TYPES.ProductoApiDataSource,       productoDs);
    container.register(TYPES.ProveedorApiDataSource,      proveedorDs);
    container.register('ProductoProveedorApiDataSource',  productoPoveedorDs);

    const categoriaRepo     = new CategoriaRepository(categoriaDs);
    const detallePedidoRepo = new DetallePedidoRepository(detallePedidoDs);
    const estadoPedidoRepo  = new EstadoPedidoRepository(estadoPedidoDs);
    const pedidoRepo        = new PedidoRepository(pedidoDs);
    const productoRepo      = new ProductoRepository(productoDs);
    const proveedorRepo     = new ProveedorRepository(proveedorDs);

    container.register(TYPES.ICategoriaRepository,    categoriaRepo);
    container.register(TYPES.IDetallePedidoRepository,detallePedidoRepo);
    container.register(TYPES.IEstadoPedidoRepository, estadoPedidoRepo);
    container.register(TYPES.IPedidoRepository,       pedidoRepo);
    container.register(TYPES.IProductoRepository,     productoRepo);
    container.register(TYPES.IProveedorRepository,    proveedorRepo);

    const categoriaUc     = new CategoriaUseCases(categoriaRepo);
    const detallePedidoUc = new DetallePedidoUseCases(detallePedidoRepo);
    const estadoPedidoUc  = new EstadoPedidoUseCases(estadoPedidoRepo);
    const pedidoUc        = new PedidoUseCases(pedidoRepo);
    const productoUc      = new ProductoUseCases(productoRepo);
    const proveedorUc     = new ProveedorUseCases(proveedorRepo);

    container.register(TYPES.ICategoriaUseCases,     categoriaUc);
    container.register(TYPES.IDetallePedidoUseCases, detallePedidoUc);
    container.register(TYPES.IEstadoPedidoUseCases,  estadoPedidoUc);
    container.register(TYPES.IPedidoUseCases,        pedidoUc);
    container.register(TYPES.IProductoUseCases,      productoUc);
    container.register(TYPES.IProveedorUseCases,     proveedorUc);

    const pedidosVm     = new PedidosViewModel();
    const productosVm   = new ProductoViewModel();
    const proveedoresVm = new ProveedoresViewModel();

    container.register(TYPES.PedidosListViewModel,    pedidosVm);
    container.register(TYPES.ProductosListViewModel,  productosVm);
    container.register(TYPES.ProveedoresListViewModel, proveedoresVm);

    const reportesVm = new ReportesViewModel(pedidoUc, productoUc, proveedorUc);
    container.register(TYPES.ReportesViewModel, reportesVm);
  });
}
