namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para la relación entre un producto y un proveedor.
    /// Incluye el precio y tiempo de entrega del proveedor, así como datos desnormalizados del producto.
    /// </summary>
    public class ProductoProveedorDTO
    {
        /// <summary>Identificador único de la relación producto-proveedor.</summary>
        public int ProductoProveedorID { get; }

        /// <summary>Identificador del producto.</summary>
        public int ProductoID { get; }

        /// <summary>Identificador del proveedor.</summary>
        public int ProveedorID { get; }

        /// <summary>Precio al que el proveedor suministra el producto.</summary>
        public decimal PrecioProveedor { get; }

        /// <summary>Tiempo estimado de entrega en días. Puede ser nulo si no está definido.</summary>
        public int? TiempoEntregaDias { get; }

        /// <summary>Nombre del producto.</summary>
        public string ProductoNombre { get; }

        /// <summary>Código interno del producto.</summary>
        public string CodigoProducto { get; }

        /// <summary>Unidad de medida del producto.</summary>
        public string UnidadMedida { get;  }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoProveedorDTO"/> con todos sus campos.
        /// </summary>
        public ProductoProveedorDTO(int productoProveedorID, int productoID, int proveedorID,
            decimal precioProveedor, int? tiempoEntregaDias, string productoNombre,
            string codigoProducto, string unidadMedida)
        {
            ProductoProveedorID = productoProveedorID;
            ProductoID = productoID;
            ProveedorID = proveedorID;
            PrecioProveedor = precioProveedor;
            TiempoEntregaDias = tiempoEntregaDias;
            ProductoNombre = productoNombre;
            CodigoProducto = codigoProducto;
            UnidadMedida = unidadMedida;
        }
    }
}