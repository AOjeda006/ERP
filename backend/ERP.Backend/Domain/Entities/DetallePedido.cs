using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa una línea de detalle dentro de un pedido a proveedor.
    /// El campo <see cref="ImporteLinea"/> es una columna calculada y almacenada en base de datos.
    /// </summary>
    public class DetallePedido
    {
        #region atributos
        private int _detallePedidoID;
        private int _pedidoID;
        private int _productoID;
        private int _cantidad;
        private decimal _precioUnitario;
        private decimal _descuento;
        private bool _activo;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public DetallePedido() { }

        /// <summary>
        /// Crea una línea de detalle de pedido con todos sus valores iniciales.
        /// </summary>
        /// <param name="detallePedidoID">Identificador único del detalle.</param>
        /// <param name="pedidoID">Identificador del pedido al que pertenece.</param>
        /// <param name="productoID">Identificador del producto solicitado.</param>
        /// <param name="cantidad">Cantidad de unidades solicitadas.</param>
        /// <param name="precioUnitario">Precio unitario acordado.</param>
        /// <param name="descuento">Porcentaje de descuento aplicado (0–100).</param>
        /// <param name="activo">Indica si la línea está activa.</param>
        public DetallePedido(int detallePedidoID, int pedidoID, int productoID,
            int cantidad, decimal precioUnitario, decimal descuento, bool activo)
        {
            _detallePedidoID = detallePedidoID;
            _pedidoID = pedidoID;
            _productoID = productoID;
            _cantidad = cantidad;
            _precioUnitario = precioUnitario;
            _descuento = descuento;
            _activo = activo;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único del detalle (clave primaria).</summary>
        [Key]
        public int DetallePedidoID { get => _detallePedidoID; set => _detallePedidoID = value; }

        /// <summary>Identificador del pedido al que pertenece esta línea.</summary>
        [Required]
        public int PedidoID { get => _pedidoID; set => _pedidoID = value; }

        /// <summary>Identificador del producto solicitado.</summary>
        [Required]
        public int ProductoID { get => _productoID; set => _productoID = value; }

        /// <summary>Cantidad de unidades solicitadas. Debe ser mayor que 0.</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Cantidad { get => _cantidad; set => _cantidad = value; }

        /// <summary>Precio unitario acordado. Debe ser mayor que 0.</summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que 0")]
        public decimal PrecioUnitario { get => _precioUnitario; set => _precioUnitario = value; }

        /// <summary>Porcentaje de descuento aplicado (0–100).</summary>
        [Required]
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100")]
        public decimal Descuento { get => _descuento; set => _descuento = value; }

        /// <summary>
        /// Importe total de la línea calculado en BD como
        /// <c>Cantidad × PrecioUnitario × (1 - Descuento / 100)</c>. Solo lectura.
        /// </summary>
        public decimal ImporteLinea { get; private set; }

        /// <summary>Indica si la línea está activa. Las líneas inactivas se excluyen de los cálculos.</summary>
        [Required]
        public bool Activo { get => _activo; set => _activo = value; }


        /// <summary>Referencia de navegación al pedido padre.</summary>
        public Pedido? Pedido { get; set; }

        /// <summary>Referencia de navegación al producto de esta línea.</summary>
        public Producto? Producto { get; set; }
        #endregion
    }
}