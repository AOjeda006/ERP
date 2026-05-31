/**
 * Contenedor de inyección de dependencias manual (Singleton).
 * @remarks Alternativa ligera a los tokens Angular DI para registrar
 * servicios instanciados manualmente (datasources, repositorios, usecases).
 * @see {@link setupDependencyInjection}
 */
export class Container {
  private static instance: Container;
  private services: Map<string, any> = new Map();

  private constructor() {}

  /** Devuelve la instancia única del contenedor (Singleton). */
  public static getInstance(): Container {
    const result = Container.instance || new Container();
    Container.instance = result;
    return result;
  }

  /**
   * Registra una instancia bajo una clave string.
   * @param key Clave única (usar constantes de {@link TYPES})
   * @param instance Instancia a registrar
   */
  public register<T>(key: string, instance: T): void {
    this.services.set(key, instance);
  }

  /**
   * Resuelve una dependencia por clave.
   * @param key Clave única del servicio
   * @returns La instancia registrada casteada al tipo `T`
   * @throws Error si la clave no está registrada
   */
  public resolve<T>(key: string): T {
    const service = this.services.get(key);
    if (!service) throw new Error(`Service ${key} not found in container`);
    return service as T;
  }

  /**
   * Indica si una clave ya está registrada.
   * @param key Clave a comprobar
   */
  public has(key: string): boolean {
    return this.services.has(key);
  }

  /** Vacía todos los registros (útil en tests). */
  public clear(): void {
    this.services.clear();
  }
}

/**
 * Claves de inyección para todos los servicios del contenedor.
 * @remarks Usar siempre estas constantes para evitar errores de typo.
 */
export const TYPES = {
  IAuthRepository: 'IAuthRepository',
  FirebaseAuthDataSource: 'FirebaseAuthDataSource',
  ILoginUseCase: 'ILoginUseCase',
  LoginViewModel: 'LoginViewModel',

  CategoriaApiDataSource:     'CategoriaApiDataSource',
  DetallePedidoApiDataSource: 'DetallePedidoApiDataSource',
  EstadoPedidoApiDataSource:  'EstadoPedidoApiDataSource',
  ProductoApiDataSource:      'ProductoApiDataSource',
  ProveedorApiDataSource:     'ProveedorApiDataSource',
  PedidoApiDataSource:        'PedidoApiDataSource',

  ICategoriaRepository:     'ICategoriaRepository',
  IDetallePedidoRepository: 'IDetallePedidoRepository',
  IEstadoPedidoRepository:  'IEstadoPedidoRepository',
  IProductoRepository:      'IProductoRepository',
  IProveedorRepository:     'IProveedorRepository',
  IPedidoRepository:        'IPedidoRepository',

  ICategoriaUseCases:     'ICategoriaUseCases',
  IDetallePedidoUseCases: 'IDetallePedidoUseCases',
  IEstadoPedidoUseCases:  'IEstadoPedidoUseCases',
  IProductoUseCases:      'IProductoUseCases',
  IProveedorUseCases:     'IProveedorUseCases',
  IPedidoUseCases:        'IPedidoUseCases',

  /** @deprecated Usar {@link IProductoUseCases} */
  IProductoUseCase:  'IProductoUseCases',
  /** @deprecated Usar {@link IProveedorUseCases} */
  IProveedorUseCase: 'IProveedorUseCases',
  /** @deprecated Usar {@link IPedidoUseCases} */
  IPedidoUseCase:    'IPedidoUseCases',

  PedidosListViewModel:     'PedidosListViewModel',
  PedidosFormViewModel:     'PedidosFormViewModel',
  ProductosListViewModel:   'ProductosListViewModel',
  ProveedoresListViewModel: 'ProveedoresListViewModel',
  ReportesViewModel:        'ReportesViewModel',
} as const;
