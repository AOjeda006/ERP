using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un pedido de compra a un proveedor.
    /// Contiene la cabecera del pedido y su colección de líneas de detalle.
    /// </summary>
    public class Pedido
    {
        #region atributos
        private int _pedidoID;
        private string _numeroPedido;
        private int _proveedorID;
        private int _estadoID;
        private DateTime _fechaPedido;
        private DateOnly? _fechaEntregaPrevista;
        private DateOnly? _fechaRecepcion;
        private string? _observaciones;
        private bool _activo;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public Pedido() { }

        /// <summary>
        /// Crea un pedido con sus valores iniciales.
        /// </summary>
        /// <param name="pedidoID">Identificador único del pedido.</param>
        /// <param name="numeroPedido">Número de pedido único.</param>
        /// <param name="proveedorID">Identificador del proveedor.</param>
        /// <param name="estadoID">Identificador del estado inicial del pedido.</param>
        /// <param name="fechaPedido">Fecha de creación del pedido.</param>
        /// <param name="activo">Indica si el pedido está activo.</param>
        public Pedido(int pedidoID, string numeroPedido, int proveedorID, int estadoID,
            DateTime fechaPedido, DateOnly? fechaEntregaPrevista, DateOnly? fechaRecepcion,
            string? observaciones, bool activo)
        {
            _pedidoID = pedidoID;
            _numeroPedido = numeroPedido;
            _proveedorID = proveedorID;
            _estadoID = estadoID;
            _fechaPedido = fechaPedido;
            _fechaEntregaPrevista = fechaEntregaPrevista;
            _fechaRecepcion = fechaRecepcion;
            _observaciones = observaciones;
            _activo = activo;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único del pedido (clave primaria).</summary>
        [Key]
        public int PedidoID { get => _pedidoID; set => _pedidoID = value; }

        /// <summary>Número de pedido único generado automáticamente. Máximo 50 caracteres.</summary>
        [Required]
        [StringLength(50)]
        public string NumeroPedido { get => _numeroPedido; set => _numeroPedido = value; }

        /// <summary>Identificador del proveedor al que se realiza el pedido.</summary>
        [Required]
        public int ProveedorID { get => _proveedorID; set => _proveedorID = value; }

        /// <summary>Identificador del estado actual del pedido.</summary>
        [Required]
        public int EstadoID { get => _estadoID; set => _estadoID = value; }

        /// <summary>
        /// Fecha y hora de creación del pedido.
        /// Por defecto se asigna con <c>GETDATE()</c> en SQL Server.
        /// </summary>
        [Required]
        public DateTime FechaPedido { get => _fechaPedido; set => _fechaPedido = value; }

        /// <summary>Fecha prevista de entrega. Opcional.</summary>
        public DateOnly? FechaEntregaPrevista { get => _fechaEntregaPrevista; set => _fechaEntregaPrevista = value; }

        /// <summary>Fecha real de recepción del pedido. Opcional.</summary>
        public DateOnly? FechaRecepcion { get => _fechaRecepcion; set => _fechaRecepcion = value; }

        /// <summary>Observaciones adicionales. Máximo 1000 caracteres. Opcional.</summary>
        [StringLength(1000)]
        public string? Observaciones { get => _observaciones; set => _observaciones = value; }

        /// <summary>
        /// Indica si el pedido está activo. Los pedidos inactivos son excluidos
        /// por el filtro global definido en <see cref="ApplicationDbContext"/>.
        /// </summary>
        [Required]
        public bool Activo { get => _activo; set => _activo = value; }

        /// <summary>Referencia de navegación al proveedor del pedido.</summary>
        public Proveedor? Proveedor { get; set; }

        /// <summary>Referencia de navegación al estado actual del pedido.</summary>
        public EstadoPedido? Estado { get; set; }

        /// <summary>Colección de líneas de detalle del pedido.</summary>
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
        #endregion

        #region Metodos
        /// <summary>
        /// Calcula el subtotal del pedido sumando el <c>ImporteLinea</c>
        /// de todas las líneas activas.
        /// </summary>
        /// <returns>Suma de los importes de las líneas activas del pedido.</returns>
        public decimal CalcularSubtotal()
        {
            return Detalles
                .Where(d => d.Activo)
                .Sum(d => d.ImporteLinea);
        }
        #endregion
    }
}