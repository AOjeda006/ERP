# NERVION'S SYSTEM — ERP de Pedidos a Proveedores

> ERP full-stack para la gestión de compras a proveedores: un backend **ASP.NET Core 8 + EF Core** sobre **SQL Server (Azure SQL)** y un cliente **Angular 18 + Material**, ambos construidos sobre **Clean Architecture** y comunicados por una API REST.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4)](https://learn.microsoft.com/ef/core/)
[![Azure SQL](https://img.shields.io/badge/Azure%20SQL-Server-0078D4?logo=microsoftsqlserver&logoColor=white)](https://azure.microsoft.com/products/azure-sql/database)
[![Angular](https://img.shields.io/badge/Angular-18-DD0031?logo=angular&logoColor=white)](https://angular.dev/)
[![Angular Material](https://img.shields.io/badge/Material-18-757575?logo=materialdesign&logoColor=white)](https://material.angular.io/)
[![Firebase](https://img.shields.io/badge/Firebase-Auth-FFCA28?logo=firebase&logoColor=black)](https://firebase.google.com/products/auth)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.4-3178C6?logo=typescript&logoColor=white)](https://www.typescriptlang.org/)

---

## Tabla de contenidos

1. [Descripción](#descripción)
2. [Características](#características)
3. [Stack tecnológico](#stack-tecnológico)
4. [Arquitectura](#arquitectura)
5. [Estructura del proyecto](#estructura-del-proyecto)
6. [Modelo de datos](#modelo-de-datos)
7. [API REST](#api-rest)
8. [Flujo de una operación de extremo a extremo](#flujo-de-una-operación-de-extremo-a-extremo)
9. [Diseño visual](#diseño-visual)
10. [Cómo ejecutar el proyecto](#cómo-ejecutar-el-proyecto)
11. [Autenticación y acceso](#autenticación-y-acceso)
12. [Pruebas](#pruebas)
13. [Decisiones técnicas destacadas](#decisiones-técnicas-destacadas)
14. [Autor](#autor)

---

## Descripción

**NERVION'S SYSTEM** es un ERP que cubre el ciclo de compras a proveedores de un
negocio: consultar el catálogo de productos y proveedores, **crear pedidos** con
sus líneas de detalle, hacer avanzar su estado a lo largo del *workflow*
(Pendiente → Enviado → Recibido → Cancelado), controlar el stock recibido en
almacén y revisar indicadores y reportes de gasto.

El proyecto se ha construido como pieza de portfolio para demostrar el desarrollo
**full-stack end-to-end** de una aplicación de gestión real: un backend en
**.NET 8** con persistencia relacional, reglas de negocio encapsuladas y una API
REST documentada con Swagger; y un cliente **Angular 18** con autenticación,
navegación protegida y una UI cuidada con Material.

Lo más destacable del diseño es que **ambos lados aplican la misma Clean
Architecture y espejan el mismo modelo** (`Pedido`, `DetallePedido`, `Producto`,
`Proveedor`, `Categoria`, `EstadoPedido`): el backend la implementa en C# y el
frontend la replica en TypeScript, manteniendo cliente y servidor perfectamente
alineados a través de los DTOs del contrato REST.

El backend es la **fuente de verdad**: las tablas maestras (proveedores,
productos, categorías y sus relaciones) son de **solo lectura** desde la API, y
únicamente los pedidos y sus líneas se gestionan vía CRUD, con **borrado lógico**.

---

## Características

### Gestión de pedidos

- **Ciclo de vida completo**: creación, edición de cabecera y líneas, cambio de
  estado y borrado lógico (*soft delete*).
- **Número de pedido autogenerado** y único (`PED-{timestamp}-{aleatorio}`), con
  estado inicial *Pendiente* asignado por el servidor.
- **Líneas de detalle** con producto, cantidad, precio unitario y descuento; el
  **importe de línea se calcula en la propia base de datos** (columna computada).
- **Reconciliación de líneas** en la edición: las líneas nuevas se insertan, las
  existentes se actualizan y las que ya no están se eliminan, en una sola
  operación.
- **Cálculo de totales en el formulario** (base imponible + IVA 21 %) en tiempo
  real conforme se editan las líneas.
- **Filtros** por proveedor y por estado, con *chips* de color por estado.

### Catálogos (solo lectura desde la API)

- **Proveedores**: consulta con datos fiscales y de contacto, y detalle de los
  productos que suministra cada uno (precio y plazo de entrega).
- **Productos**: catálogo con categoría, unidad de medida y precio de referencia,
  con búsqueda y filtro por categoría.
- **Relación producto–proveedor**: precio y tiempo de entrega específicos por
  proveedor, usados al construir un pedido.

### Almacén y analítica

- **Almacén**: vista de stock derivada de los pedidos **recibidos**, agrupando las
  unidades por producto y categoría.
- **Dashboard**: KPIs en vivo (total de pedidos, productos y proveedores).
- **Reportes**: indicadores agregados combinando pedidos, productos y proveedores.

### Transversales

- **Autenticación** con Firebase Authentication (email/contraseña), rutas
  protegidas por *guards* y carga *lazy* de cada módulo.
- **Interceptores HTTP**: inyección del *token* Bearer, manejo centralizado de
  errores (con redirección a login ante 401) e indicador global de carga.
- **Notificaciones** *toast* unificadas (éxito / error / info / aviso).
- **API documentada** con Swagger en desarrollo.

---

## Stack tecnológico

### Backend (`/backend`)

| Categoría        | Tecnología                                                           |
| ---------------- | -------------------------------------------------------------------- |
| Lenguaje         | C# 12 sobre **.NET 8**                                                |
| Host web         | ASP.NET Core (Kestrel) + Controllers                                 |
| Arquitectura     | Clean Architecture: `Domain` · `Data` · `DI` · `API`                 |
| Persistencia     | **Entity Framework Core 8** sobre **SQL Server / Azure SQL**         |
| Casos de uso     | Una clase *UseCase* por agregado tras su interfaz                    |
| Mapeo            | *Mappers* entidad → DTO (registrados como *singleton*)               |
| Inyección de dep.| `Microsoft.Extensions.DependencyInjection` (`Container` centralizado)|
| Documentación    | **Swashbuckle / Swagger** 8                                          |
| Resiliencia      | `EnableRetryOnFailure` + `CommandTimeout` para Azure SQL             |
| Pruebas          | **xUnit** + **Moq** + EF Core **InMemory** + FluentAssertions        |

### Frontend (`/frontend`)

| Categoría        | Tecnología                                                           |
| ---------------- | -------------------------------------------------------------------- |
| Lenguaje         | **TypeScript 5.4** (modo estricto)                                   |
| Framework        | **Angular 18** (componentes *standalone*)                            |
| UI               | **Angular Material 18** + SCSS + tema propio                         |
| Estado           | **MVVM** con `ViewModel` + RxJS (`BehaviorSubject` / `Observable`)   |
| Navegación       | `@angular/router` con rutas *lazy* y *guards*                        |
| Autenticación    | **Firebase Auth** vía **AngularFire 18**                             |
| Inyección de dep.| Contenedor propio de *singletons* (`core/container/`)                |
| HTTP             | `HttpClient` + interceptores (auth, error, loading)                  |
| Pruebas          | **Karma + Jasmine**                                                  |

---

## Arquitectura

Dos aplicaciones independientes que se comunican exclusivamente por una **API
REST**. El servidor es autoritativo; el cliente refleja su estado y replica el
mismo modelo de dominio.

```
        ┌───────────────────────────────┐         ┌───────────────────────────────┐
        │     CLIENTE (Angular 18)      │         │      SERVIDOR (.NET 8)        │
        │                               │         │                               │
        │  presentation (MVVM + RxJS)   │         │  API (Controllers + Swagger)  │
        │  data (DataSources + Repos)   │ ──────► │  Domain (UseCases + Mappers)  │
        │  domain (entidades + casos)   │  HTTP   │  Data (EF Core + Repos)       │
        │  core (DI + interceptores)    │  /api   │  DI (Container de servicios)  │
        └───────────────────────────────┘         └───────────────────────────────┘
                     mismo modelo de dominio espejado (Pedido, Producto, Proveedor…)
```

Ambos lados siguen **Clean Architecture**: las dependencias apuntan siempre hacia
el dominio, que no conoce ni a EF Core, ni a HTTP, ni a la UI.

### Backend — capas

- **`Domain`** — núcleo puro, sin dependencias de ASP.NET ni EF:
  - **Entidades**: `Producto`, `CategoriaProducto`, `Proveedor`,
    `ProductoProveedor`, `EstadoPedido`, `Pedido` (raíz del agregado) y
    `DetallePedido`.
  - **DTOs** + **interfaces** de repositorios, casos de uso y *mappers*: son la
    fuente de verdad del contrato.
  - **Casos de uso**: orquestan repositorios y *mappers*, encapsulando las reglas
    de negocio (generación del número de pedido, transición de estado,
    *soft delete*…) y devolviendo DTOs a la capa de API.
  - **Excepciones de dominio** (`DomainException`, `NotFoundException`).
- **`Data`** — implementación de persistencia con EF Core:
  - **`ApplicationDbContext`**, configuraciones **Fluent API** por entidad
    (claves, índices, longitudes, columna computada, *defaults*) y un
    **filtro de consulta global** que excluye los pedidos inactivos.
  - **Repositorios** que aplican `Include`/`AsNoTracking` y devuelven entidades de
    dominio; el `PedidoRepository` resuelve la reconciliación de líneas.
- **`DI`** — `Container.RegisterServices` registra de forma centralizada el
  `DbContext`, repositorios y casos de uso (`Scoped`) y los *mappers* (`Singleton`).
- **`API`** — controladores REST finos que construyen las entidades desde los
  *request models*, delegan en los casos de uso, exponen Swagger y aplican CORS.

### Frontend — capas

- **`core`** — contenedor de inyección de dependencias propio, *guards*,
  interceptores HTTP, servicios globales (auth, loading, notificaciones) y modelos.
- **`domain`** (por *feature*) — entidades TypeScript, interfaces de repositorios
  y casos de uso, y sus implementaciones.
- **`data`** (por *feature*) — *DataSources* (llamadas `HttpClient` sobre una base
  común) y repositorios que mapean DTO → entidad.
- **`presentation`** — patrón **MVVM**: un `ViewModel` por área expone estado
  observable con RxJS, y los componentes *standalone* lo consumen vía `async` pipe.

---

## Estructura del proyecto

```
ERP-PedidosProveedores/
├── backend/
│   └── ERP.Backend/                    # Solución .NET (ERP.Backend.slnx)
│       ├── Domain/                     # Núcleo de negocio (sin dependencias web/EF)
│       │   ├── Entities/               # Producto, Proveedor, Pedido, DetallePedido…
│       │   ├── DTOs/                   # Contrato de datos con el cliente
│       │   ├── Interfaces/             # IRepository · IUseCases · IMappers
│       │   ├── UseCases/               # Lógica de aplicación por agregado
│       │   ├── Mappers/                # Entidad → DTO
│       │   └── Exceptions/             # DomainException, NotFoundException
│       ├── Data/
│       │   ├── DataSources/            # ApplicationDbContext
│       │   ├── Configurations/         # Fluent API por entidad
│       │   ├── Repositories/           # Implementaciones de los repositorios
│       │   └── Network/                # DbConfiguration (retry/timeout Azure SQL)
│       ├── DI/                         # Container.RegisterServices
│       ├── API/
│       │   ├── Controllers/            # Categorias, Productos, Proveedores, Pedidos…
│       │   ├── Models/                 # Request models (Crear/Actualizar…)
│       │   └── Program.cs              # Arranque, CORS, Swagger
│       └── Backend.Test/               # xUnit (controllers, repos, mappers, usecases)
├── frontend/
│   └── src/app/
│       ├── core/                       # DI Container, guards, interceptores, servicios
│       ├── shared/                     # Componentes, directivas y pipes reutilizables
│       └── features/                   # pedidos · productos · proveedores · almacen ·
│                                       #   dashboard · reportes · auth
└── bbdd/                               # Scripts SQL de creación y datos de prueba
```

---

## Modelo de datos

Siete tablas; las maestras son de solo lectura desde la API y solo `Pedidos` /
`DetallesPedido` se gestionan vía CRUD (con borrado lógico):

```
┌────────────────────┐        ┌────────────────────┐        ┌────────────────────┐
│ CategoriasProducto │ 1    * │     Productos      │ *    1 │    Proveedores     │
├────────────────────┤────────├────────────────────┤        ├────────────────────┤
│ CategoriaID (PK)   │        │ ProductoID (PK)    │        │ ProveedorID (PK)   │
│ NombreCategoria (U)│        │ CategoriaID (FK)   │        │ CIF (U)            │
└────────────────────┘        │ CodigoProducto (U) │        │ RazonSocial        │
                              │ PrecioUnitario     │        │ …contacto          │
            ┌──────────────────┴────────┐         └──────────┬─────────┘
            │   ProductosProveedores    │ *────────────────* │
            ├───────────────────────────┤  (N:M producto↔proveedor, precio y plazo)
            │ ProductoProveedorID (PK)  │
            │ ProductoID (FK)           │
            │ ProveedorID (FK)          │        ┌────────────────────┐
            │ PrecioProveedor           │        │   EstadosPedido    │
            └───────────────────────────┘        ├────────────────────┤
                                                 │ EstadoID (PK)      │
┌────────────────────┐        ┌──────────────────┴─┐  *   1 │ NombreEstado (U)   │
│   DetallesPedido   │ *    1 │      Pedidos       │────────│ OrdenEstado        │
├────────────────────┤────────├────────────────────┤        └────────────────────┘
│ DetallePedidoID(PK)│        │ PedidoID (PK)      │ *    1   ┌─► Proveedores
│ PedidoID (FK)      │        │ NumeroPedido (U)   │──────────┘
│ ProductoID (FK)    │        │ ProveedorID (FK)   │
│ Cantidad           │        │ EstadoID (FK)      │
│ PrecioUnitario     │        │ FechaPedido        │
│ Descuento          │        │ FechaEntregaPrev.  │
│ ImporteLinea ⚙      │        │ FechaRecepcion     │
│ Activo             │        │ Activo             │
└────────────────────┘        └────────────────────┘
   ON DELETE CASCADE             ⚙ columna computada PERSISTED
```

**Decisiones de modelado relevantes:**

- **Borrado lógico**: `Pedidos` y `DetallesPedido` no se borran físicamente; se
  marca `Activo = 0`. Un **filtro de consulta global** sobre `Pedido` excluye los
  inactivos en *todas* las consultas, sin tener que repetir el `WHERE`.
- **`ImporteLinea` como columna computada PERSISTED**: el importe
  (`Cantidad × PrecioUnitario × (1 − Descuento/100)`) lo calcula y persiste **SQL
  Server**, no la aplicación; es la fuente de verdad y se indexa/lee como una
  columna normal.
- **Unicidad garantizada por la base de datos**: índices únicos en
  `NumeroPedido`, `CodigoProducto`, `CIF`, `NombreCategoria`, `NombreEstado` y en
  el par `(ProductoID, ProveedorID)` — la integridad la asegura SQL, no el código.
- **Fechas de pedido en `date`**: `FechaEntregaPrevista` y `FechaRecepcion` se
  modelan como `DateOnly` y los *mappers* las convierten a `DateTime` en el DTO.
- **Índices secundarios** sobre las FK y campos de filtro (`ProveedorID`,
  `EstadoID`, `FechaPedido`, `Activo`…) para que *joins* y filtros no requieran
  escaneos completos.
- **Borrado en cascada controlado**: las líneas siguen a su pedido
  (`ON DELETE CASCADE`); el resto de FK usan `Restrict` para no borrar maestros.

---

## API REST

Base: `/api`. Documentación interactiva en `/swagger` (entorno de desarrollo).

### Catálogos (solo lectura)

| Método | Ruta                                       | Acción                                  |
| ------ | ------------------------------------------ | --------------------------------------- |
| `GET`  | `/api/categorias`                          | Lista de categorías                     |
| `GET`  | `/api/categorias/{id}`                     | Categoría por id                        |
| `GET`  | `/api/productos`                           | Catálogo completo                       |
| `GET`  | `/api/productos/{id}`                      | Producto por id                         |
| `GET`  | `/api/productos/categoria/{categoriaId}`   | Productos de una categoría              |
| `GET`  | `/api/proveedores`                         | Lista de proveedores                    |
| `GET`  | `/api/proveedores/{id}`                    | Proveedor por id                        |
| `GET`  | `/api/productosproveedores/proveedor/{id}` | Productos que suministra un proveedor   |
| `GET`  | `/api/productosproveedores/producto/{id}`  | Proveedores que ofrecen un producto     |
| `GET`  | `/api/estadospedido`                       | Estados del *workflow*                  |
| `GET`  | `/api/estadospedido/{id}`                  | Estado por id                           |

### Pedidos (CRUD)

| Método  | Ruta                            | Acción                                            |
| ------- | ------------------------------- | ------------------------------------------------- |
| `GET`   | `/api/pedidos`                  | Pedidos activos (orden descendente por fecha)     |
| `GET`   | `/api/pedidos/{id}`             | Detalle completo del pedido con sus líneas        |
| `GET`   | `/api/pedidos/proveedor/{id}`   | Pedidos de un proveedor                           |
| `GET`   | `/api/pedidos/estado/{id}`      | Pedidos en un estado                              |
| `GET`   | `/api/pedidos/recibidos`        | Pedidos recibidos (para la vista de almacén)      |
| `POST`  | `/api/pedidos`                  | Crea un pedido (genera número y estado inicial)   |
| `PUT`   | `/api/pedidos/{id}`             | Actualiza cabecera y reconcilia líneas            |
| `PATCH` | `/api/pedidos/{id}/estado`      | Cambia el estado del pedido                       |
| `DELETE`| `/api/pedidos/{id}`             | Borrado lógico (`Activo = 0`)                     |

---

## Flujo de una operación de extremo a extremo

Crear un pedido recorre las dos aplicaciones y las cuatro capas de cada una:

```
 CLIENTE (Angular)                                  SERVIDOR (.NET)
 ─────────────────                                  ───────────────
 PedidosFormComponent  (rellena cabecera + líneas, calcula totales)
   └─ PedidosViewModel.crearPedido(payload)
        └─ PedidoUseCases.createAsync(payload)
             └─ PedidoRepository (data)
                  └─ PedidoApiDataSource.create ──HTTP POST /api/pedidos──►  PedidosController.Create(request)
                                                                                └─ genera NumeroPedido, EstadoID=1,
                                                                                   construye Pedido + Detalles
                                                                                   └─ IPedidoUseCase.CreateAsync(pedido)
                                                                                        └─ PedidoRepository.CreateAsync
                                                                                             └─ EF Core → INSERT
                                                                                                (SQL calcula ImporteLinea)
                       ◄────────────── 201 Created + PedidoDTO ──────────────┘
   El ViewModel recarga la lista y notifica el éxito; la tabla se actualiza
   de forma reactiva vía el Observable de pedidos.
```

El detalle del pedido (`GET /api/pedidos/{id}`) embebe el proveedor y el estado,
e incluye únicamente las **líneas activas**, ya mapeadas a DTOs por el
`PedidoMapper`.

---

## Diseño visual

Tema Material 3 personalizado, claro, con sidebar oscuro y acento índigo/violeta;
tipografía **Outfit** (interfaz) combinada con **JetBrains Mono** (datos numéricos):

| Token              | Hex       | Uso                                          |
| ------------------ | --------- | -------------------------------------------- |
| `--clr-bg`         | `#F0F2F8` | Fondo general                                |
| `--clr-surface`    | `#FFFFFF` | Tarjetas y superficies                       |
| `--clr-sidebar`    | `#0D1117` | Barra lateral (oscura)                       |
| `--clr-accent`     | `#6366F1` | Color primario, llamadas a la acción         |
| `--clr-accent2`    | `#8B5CF6` | Acento secundario / degradados               |
| `--clr-text`       | `#0F172A` | Texto principal                              |
| `--clr-muted`      | `#64748B` | Texto secundario y etiquetas                 |
| `--clr-success`    | `#10B981` | Estado positivo / *toasts* de éxito          |
| `--clr-danger`     | `#EF4444` | Errores y acciones destructivas              |

---

## Cómo ejecutar el proyecto

Necesitas el backend en marcha y una base de datos accesible **antes** de arrancar
el cliente.

### Requisitos

- **.NET 8 SDK**
- **Node.js 20+** y npm
- **SQL Server** (local, Express o Azure SQL)
- Un proyecto de **Firebase** con autenticación por email/contraseña habilitada

### 1. Base de datos

Ejecuta los scripts de `bbdd/` sobre tu instancia de SQL Server para crear el
esquema y los datos de prueba:

```
bbdd/01_CREACION_BBDD.sql      # tablas, índices y estados iniciales
bbdd/02_DATOS_PRUEBA.sql       # datos de ejemplo (proveedores, productos…)
```

### 2. Backend (.NET 8)

Configura la cadena de conexión en `backend/ERP.Backend/API/appsettings.json`
(el archivo versionado contiene **solo un placeholder**):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=ERP_PedidosProveedores;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True;"
  }
}
```

> Para desarrollo local puedes poner tus credenciales reales en
> `appsettings.Development.json` (ignorado por git) en lugar de tocar el archivo
> versionado.

Restaura y arranca:

```bash
cd backend/ERP.Backend/API
dotnet restore
dotnet run
```

La API queda disponible en `http://localhost:5141` y Swagger en
`http://localhost:5141/swagger`.

### 3. Frontend (Angular 18)

Instala dependencias y configura el entorno (existe `environment.example.ts` como
plantilla; los valores versionados son **placeholders**):

```bash
cd frontend
npm install
```

`src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: '/api', // en desarrollo se resuelve vía proxy
  firebase: {
    apiKey: 'TU_API_KEY',
    authDomain: 'TU_PROJECT_ID.firebaseapp.com',
    projectId: 'TU_PROJECT_ID',
    storageBucket: 'TU_PROJECT_ID.appspot.com',
    messagingSenderId: 'TU_MESSAGING_SENDER_ID',
    appId: 'TU_APP_ID',
    measurementId: 'TU_MEASUREMENT_ID',
  },
};
```

Arranca el cliente (el proxy `src/proxy.conf.json` redirige `/api` al backend
local en `http://localhost:5141`):

```bash
npm start
```

La aplicación queda disponible en `http://localhost:4200`.

---

## Autenticación y acceso

El acceso se controla con **Firebase Authentication** (email/contraseña). La
aplicación protege todas las rutas del *dashboard* con un *guard* que redirige a
`/login` si no hay sesión, e inyecta el *token* Bearer de Firebase en cada
petición a la API mediante un interceptor.

> Crea un usuario desde la consola de Firebase (o habilita el registro) para
> iniciar sesión. Las claves de Firebase no se incluyen en el repositorio: deben
> configurarse en los archivos de entorno como se indica arriba.

---

## Pruebas

```bash
# Backend — xUnit + Moq + EF Core InMemory
cd backend/ERP.Backend
dotnet test

# Frontend — Karma + Jasmine
cd frontend
npm test
```

La suite del backend cubre controladores (con `Moq`), repositorios (sobre una base
de datos en memoria), *mappers* y casos de uso.

---

## Decisiones técnicas destacadas

- **Clean Architecture espejada en ambos lados**: el mismo modelo y la misma
  separación de capas (dominio → datos → presentación) se implementan en C# y en
  TypeScript, comunicados por DTOs. El dominio nunca depende de EF, HTTP ni la UI.
- **Borrado lógico con filtro global**: los pedidos se desactivan en vez de
  borrarse, y un *global query filter* de EF Core los excluye automáticamente de
  todas las consultas — la regla vive en un único sitio.
- **Cálculo en la base de datos**: `ImporteLinea` es una **columna computada
  PERSISTED**; el importe es coherente por construcción y no depende de que la
  aplicación lo recalcule.
- **Maestros de solo lectura**: proveedores, productos y categorías se exponen
  solo por `GET`; el CRUD se limita a pedidos y sus líneas, reduciendo la
  superficie de escritura y los riesgos de integridad.
- **Reconciliación de líneas en la edición**: actualizar un pedido compara las
  líneas entrantes con las persistidas e **inserta, actualiza o elimina** según
  corresponda, en una sola operación de repositorio.
- **Resiliencia frente a Azure SQL**: la conexión usa `EnableRetryOnFailure`
  (reintentos con *backoff*) y `CommandTimeout`, para tolerar los cortes
  transitorios típicos de una base de datos en la nube.
- **Lecturas sin *tracking***: los repositorios usan `AsNoTracking` e `Include`
  selectivos, devolviendo grafos listos para mapear sin sobrecarga del *change
  tracker*.
- **MVVM con RxJS en el cliente**: cada `ViewModel` expone el estado como
  `Observable`; los componentes lo consumen con el `async` pipe, evitando
  suscripciones manuales y fugas.
- **Capa HTTP con interceptores**: autenticación (token Bearer de Firebase),
  manejo de errores centralizado (con redirección a login ante 401) e indicador de
  carga global están desacoplados de los componentes.
- **Inyección de dependencias explícita**: el backend centraliza el registro en
  `Container.RegisterServices` con ciclos de vida deliberados (`Scoped` para
  repositorios y casos de uso, `Singleton` para *mappers* sin estado); el frontend
  cablea su propio contenedor en el arranque mediante `APP_INITIALIZER`.
- **Carga *lazy* y rutas protegidas**: cada módulo del *dashboard* se carga bajo
  demanda detrás de un *guard* de autenticación.

---

## Autor

**Andrés Ojeda Rodríguez**
[andresojedarodriguez@gmail.com](mailto:andresojedarodriguez@gmail.com)


---

## Licencia

Este proyecto está licenciado bajo la **PolyForm Noncommercial License 1.0.0**.
Puedes ver, ejecutar, estudiar y modificar el código con fines **no comerciales**
(estudio personal, educación, evaluación), pero **cualquier uso comercial requiere
permiso escrito del autor**. Consulta el archivo [LICENSE.md](LICENSE.md) para los
términos completos.

© 2026 Andrés Ojeda Rodríguez. Todos los derechos no concedidos expresamente quedan reservados.
