using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa la relación entre un producto y un proveedor,
    /// incluyendo el precio y tiempo de entrega específicos de ese proveedor para ese producto.
    /// </summary>
    public class ProductoProveedor
    {
        #region atributos
        private int _productoProveedorID;
        private int _productoID;
        private int _proveedorID;
        private decimal _precioProveedor;
        private int? _tiempoEntregaDias;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public ProductoProveedor() { }

        /// <summary>
        /// Crea una relación entre producto y proveedor con sus valores iniciales.
        /// </summary>
        /// <param name="productoProveedorID">Identificador único de la relación.</param>
        /// <param name="productoID">Identificador del producto.</param>
        /// <param name="proveedorID">Identificador del proveedor.</param>
        /// <param name="precioProveedor">Precio al que el proveedor suministra el producto.</param>
        /// <param name="tiempoEntregaDias">Tiempo estimado de entrega en días naturales (opcional).</param>
        public ProductoProveedor(int productoProveedorID, int productoID, int proveedorID,
            decimal precioProveedor, int? tiempoEntregaDias)
        {
            _productoProveedorID = productoProveedorID;
            _productoID = productoID;
            _proveedorID = proveedorID;
            _precioProveedor = precioProveedor;
            _tiempoEntregaDias = tiempoEntregaDias;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único de la relación (clave primaria).</summary>
        [Key]
        public int ProductoProveedorID { get => _productoProveedorID; set => _productoProveedorID = value; }

        /// <summary>Identificador del producto.</summary>
        [Required]
        public int ProductoID { get => _productoID; set => _productoID = value; }

        /// <summary>Identificador del proveedor.</summary>
        [Required]
        public int ProveedorID { get => _proveedorID; set => _proveedorID = value; }

        /// <summary>Precio al que el proveedor suministra el producto. Debe ser mayor que 0.</summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del proveedor debe ser mayor que 0")]
        public decimal PrecioProveedor { get => _precioProveedor; set => _precioProveedor = value; }

        /// <summary>
        /// Tiempo estimado de entrega en días naturales.
        /// Puede ser nulo si el proveedor no ha especificado este dato.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El tiempo de entrega debe ser positivo")]
        public int? TiempoEntregaDias { get => _tiempoEntregaDias; set => _tiempoEntregaDias = value; }


        /// <summary>Referencia de navegación al producto.</summary>
        public Producto Producto { get; set; }

        /// <summary>Referencia de navegación al proveedor.</summary>
        public Proveedor Proveedor { get; set; }
        #endregion
    }
}