-- ============================================
-- ESQUEMA: ERP_PedidosProveedores (SIMPLIFICADO)
-- DESCRIPCIÓN: Sistema de gestión de pedidos a proveedores
-- AUTOR: Andrés Ojeda (Project Manager)
-- FECHA: 2026-02-18
-- NOTAS: Las tablas maestras (Proveedores, Productos, Categorías,
--        ProductosProveedores) son de solo lectura desde la API.
--        Solo Pedidos y DetallesPedido se gestionan via CRUD.
--        El borrado es lógico (campo Activo = 0).
-- ============================================

-- =============================================
-- TABLAS MAESTRAS (solo lectura desde la API)
-- Se rellenan mediante scripts de inserción
-- =============================================

-- TABLA: CategoriasProducto
-- Clasificación de productos
CREATE TABLE CategoriasProducto (
    CategoriaID     INT PRIMARY KEY IDENTITY(1,1),
    NombreCategoria NVARCHAR(100) NOT NULL UNIQUE,
    Descripcion     NVARCHAR(500) NULL
);

-- TABLA: Proveedores
-- Datos de los proveedores
CREATE TABLE Proveedores (
    ProveedorID      INT PRIMARY KEY IDENTITY(1,1),
    CIF              VARCHAR(15) NOT NULL UNIQUE,
    RazonSocial      NVARCHAR(255) NOT NULL,
    NombreComercial  NVARCHAR(255) NULL,
    Direccion        NVARCHAR(500) NULL,
    Ciudad           NVARCHAR(100) NULL,
    Provincia        NVARCHAR(100) NULL,
    Telefono         VARCHAR(20) NULL,
    Email            VARCHAR(255) NULL,
    PersonaContacto  NVARCHAR(255) NULL
);

-- TABLA: Productos
-- Catálogo de productos disponibles
CREATE TABLE Productos (
    ProductoID      INT PRIMARY KEY IDENTITY(1,1),
    CategoriaID     INT NOT NULL,
    CodigoProducto  VARCHAR(50) NOT NULL UNIQUE,
    NombreProducto  NVARCHAR(255) NOT NULL,
    Descripcion     NVARCHAR(500) NULL,
    UnidadMedida    NVARCHAR(50) NOT NULL,       -- Unidad, Kg, Litro, Caja...
    PrecioUnitario  DECIMAL(18,2) NOT NULL,       -- Precio de referencia

    CONSTRAINT FK_Productos_Categorias
        FOREIGN KEY (CategoriaID) REFERENCES CategoriasProducto(CategoriaID),
    CONSTRAINT CHK_PrecioUnitario
        CHECK (PrecioUnitario >= 0)
);

-- TABLA: ProductosProveedores
-- Relación N:M entre Productos y Proveedores (qué proveedor vende qué producto y a qué precio)
CREATE TABLE ProductosProveedores (
    ProductoProveedorID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID          INT NOT NULL,
    ProveedorID         INT NOT NULL,
    PrecioProveedor     DECIMAL(18,2) NOT NULL,
    TiempoEntregaDias   INT NULL,

    CONSTRAINT FK_PP_Productos
        FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    CONSTRAINT FK_PP_Proveedores
        FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID),
    CONSTRAINT UQ_ProductoProveedor
        UNIQUE (ProductoID, ProveedorID),
    CONSTRAINT CHK_PrecioProveedor
        CHECK (PrecioProveedor > 0)
);

-- =============================================
-- TABLA DE CATÁLOGO DE ESTADOS
-- =============================================

-- TABLA: EstadosPedido
-- Estados posibles de un pedido en el workflow
CREATE TABLE EstadosPedido (
    EstadoID     INT PRIMARY KEY IDENTITY(1,1),
    NombreEstado NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion  NVARCHAR(255) NULL,
    OrdenEstado  INT NOT NULL              -- Para ordenar en el workflow
);

-- =============================================
-- TABLAS CRUD (gestionadas desde la API)
-- Borrado lógico mediante campo Activo
-- =============================================

-- TABLA: Pedidos
-- Cabecera de pedidos a proveedores
CREATE TABLE Pedidos (
    PedidoID             INT PRIMARY KEY IDENTITY(1,1),
    NumeroPedido         VARCHAR(50) NOT NULL UNIQUE,  -- Ej: PED-2026-00001
    ProveedorID          INT NOT NULL,
    EstadoID             INT NOT NULL,
    FechaPedido          DATETIME NOT NULL DEFAULT GETDATE(),
    FechaEntregaPrevista DATE NULL,
    FechaRecepcion       DATE NULL,                     -- Se rellena al marcar como Recibido
    Observaciones        NVARCHAR(1000) NULL,
    Activo               BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Pedidos_Proveedores
        FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID),
    CONSTRAINT FK_Pedidos_Estados
        FOREIGN KEY (EstadoID) REFERENCES EstadosPedido(EstadoID)
);

-- TABLA: DetallesPedido
-- Líneas de detalle de cada pedido (cada producto dentro del pedido)
CREATE TABLE DetallesPedido (
    DetallePedidoID INT PRIMARY KEY IDENTITY(1,1),
    PedidoID        INT NOT NULL,
    ProductoID      INT NOT NULL,
    Cantidad        INT NOT NULL,
    PrecioUnitario  DECIMAL(18,2) NOT NULL,
    Descuento       DECIMAL(5,2) NOT NULL DEFAULT 0,     -- Porcentaje
    ImporteLinea    AS (Cantidad * PrecioUnitario * (1 - Descuento / 100)) PERSISTED,
    Activo          BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Detalles_Pedidos
        FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID) ON DELETE CASCADE,
    CONSTRAINT FK_Detalles_Productos
        FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    CONSTRAINT CHK_Cantidad
        CHECK (Cantidad > 0),
    CONSTRAINT CHK_PrecioUnitarioDetalle
        CHECK (PrecioUnitario > 0),
    CONSTRAINT CHK_Descuento
        CHECK (Descuento >= 0 AND Descuento <= 100)
);

-- =============================================
-- ÍNDICES PARA OPTIMIZACIÓN
-- =============================================
CREATE INDEX IDX_Productos_Categoria          ON Productos(CategoriaID);
CREATE INDEX IDX_Pedidos_Proveedor            ON Pedidos(ProveedorID);
CREATE INDEX IDX_Pedidos_Estado               ON Pedidos(EstadoID);
CREATE INDEX IDX_Pedidos_FechaPedido          ON Pedidos(FechaPedido);
CREATE INDEX IDX_Pedidos_Activo               ON Pedidos(Activo);
CREATE INDEX IDX_DetallesPedido_Pedido        ON DetallesPedido(PedidoID);
CREATE INDEX IDX_DetallesPedido_Activo        ON DetallesPedido(Activo);
CREATE INDEX IDX_ProductosProveedores_Prod    ON ProductosProveedores(ProductoID);
CREATE INDEX IDX_ProductosProveedores_Prov    ON ProductosProveedores(ProveedorID);

-- =============================================
-- DATOS INICIALES: Estados de pedido
-- =============================================
INSERT INTO EstadosPedido (NombreEstado, Descripcion, OrdenEstado) VALUES
    ('Pendiente',  'Pedido creado, pendiente de envío al proveedor', 1),
    ('Enviado',    'Pedido enviado al proveedor',                    2),
    ('Recibido',   'Pedido recibido en almacén',                     3),
    ('Cancelado',  'Pedido cancelado',                               4);
