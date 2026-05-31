using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un producto del catálogo.
    /// </summary>
    public class Producto
    {
        #region atributos
        private int _productoID;
        private int _categoriaID;
        private string _codigoProducto;
        private string _nombreProducto;
        private string? _descripcion;
        private string _unidadMedida;
        private decimal _precioUnitario;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public Producto() { }

        /// <summary>
        /// Crea un producto con sus valores iniciales.
        /// </summary>
        /// <param name="productoID">Identificador único del producto.</param>
        /// <param name="categoriaID">Identificador de la categoría del producto.</param>
        /// <param name="codigoProducto">Código interno único del producto.</param>
        /// <param name="nombreProducto">Nombre descriptivo del producto.</param>
        /// <param name="descripcion">Descripción extendida opcional del producto.</param>
        /// <param name="unidadMedida">Unidad de medida del producto.</param>
        /// <param name="precioUnitario">Precio unitario de referencia.</param>
        public Producto(int productoID, int categoriaID, string codigoProducto,
            string nombreProducto, string? descripcion, string unidadMedida, decimal precioUnitario)
        {
            _productoID = productoID;
            _categoriaID = categoriaID;
            _codigoProducto = codigoProducto;
            _nombreProducto = nombreProducto;
            _descripcion = descripcion;
            _unidadMedida = unidadMedida;
            _precioUnitario = precioUnitario;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único del producto (clave primaria).</summary>
        [Key]
        public int ProductoID { get => _productoID; set => _productoID = value; }

        /// <summary>Identificador de la categoría a la que pertenece el producto.</summary>
        [Required]
        public int CategoriaID { get => _categoriaID; set => _categoriaID = value; }

        /// <summary>Código interno único del producto. Máximo 50 caracteres.</summary>
        [Required]
        [StringLength(50)]
        public string CodigoProducto { get => _codigoProducto; set => _codigoProducto = value; }

        /// <summary>Nombre descriptivo del producto. Máximo 255 caracteres.</summary>
        [Required]
        [StringLength(255)]
        public string NombreProducto { get => _nombreProducto; set => _nombreProducto = value; }

        /// <summary>Descripción extendida del producto. Máximo 500 caracteres. Opcional.</summary>
        [StringLength(500)]
        public string? Descripcion { get => _descripcion; set => _descripcion = value; }

        /// <summary>Unidad de medida. Máximo 50 caracteres.</summary>
        [Required]
        [StringLength(50)]
        public string UnidadMedida { get => _unidadMedida; set => _unidadMedida = value; }

        /// <summary>Precio unitario de referencia. Debe ser mayor o igual a 0.</summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser positivo")]
        public decimal PrecioUnitario { get => _precioUnitario; set => _precioUnitario = value; }


        /// <summary>Referencia de navegación a la categoría del producto.</summary>
        public CategoriaProducto? Categoria { get; set; }

        /// <summary>Colección de relaciones con proveedores que suministran este producto.</summary>
        public ICollection<ProductoProveedor> ProductosProveedores { get; set; } = new List<ProductoProveedor>();
        #endregion
    }
}