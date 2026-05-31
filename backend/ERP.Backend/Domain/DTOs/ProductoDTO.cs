namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para un producto del catálogo.
    /// Incluye el nombre de la categoría de forma desnormalizada.
    /// </summary>
    public class ProductoDTO
    {
        /// <summary>Identificador único del producto.</summary>
        public int ProductoID { get; }

        /// <summary>Código interno del producto.</summary>
        public string CodigoProducto { get; }

        /// <summary>Nombre descriptivo del producto.</summary>
        public string NombreProducto { get; }

        /// <summary>Descripción detallada del producto. Puede ser nula.</summary>
        public string? Descripcion { get; }

        /// <summary>Unidad de medida del producto.</summary>
        public string UnidadMedida { get; }

        /// <summary>Precio unitario de referencia del producto.</summary>
        public decimal PrecioUnitario { get; }

        /// <summary>Nombre de la categoría a la que pertenece el producto.</summary>
        public string CategoriaNombre { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoDTO"/> con todos sus campos.
        /// </summary>
        public ProductoDTO(int productoID, string codigoProducto, string nombreProducto,
            string? descripcion, string unidadMedida, decimal precioUnitario, string categoriaNombre)
        {
            ProductoID = productoID;
            CodigoProducto = codigoProducto;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            UnidadMedida = unidadMedida;
            PrecioUnitario = precioUnitario;
            CategoriaNombre = categoriaNombre;
        }
    }
}