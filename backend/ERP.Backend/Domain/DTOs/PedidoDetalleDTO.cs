namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos con el detalle completo de un pedido,
    /// incluyendo la información de proveedor, estado y todas las líneas activas.
    /// Se utiliza en las respuestas de detalle individual y en el listado de pedidos recibidos.
    /// </summary>
    public class PedidoDetalleDTO
    {
        /// <summary>Identificador único del pedido.</summary>
        public int PedidoID { get; }

        /// <summary>Número de pedido generado automáticamente.</summary>
        public string NumeroPedido { get; }

        /// <summary>Datos del proveedor al que se realizó el pedido.</summary>
        public ProveedorDTO Proveedor { get; }

        /// <summary>Estado actual del pedido.</summary>
        public EstadoPedidoDTO Estado { get; }

        /// <summary>Fecha y hora en que se creó el pedido.</summary>
        public DateTime FechaPedido { get; }

        /// <summary>Fecha prevista de entrega. Puede ser nula.</summary>
        public DateTime? FechaEntregaPrevista { get; }

        /// <summary>Fecha real de recepción del pedido. Puede ser nula.</summary>
        public DateTime? FechaRecepcion { get; }

        /// <summary>Observaciones adicionales sobre el pedido.</summary>
        public string? Observaciones { get; }

        /// <summary>Indica si el pedido está activo.</summary>
        public bool Activo { get; }

        /// <summary>Lista de líneas de detalle activas del pedido.</summary>
        public List<DetallePedidoDTO> Detalles { get; }

        /// <summary>Suma del importe de todas las líneas activas del pedido.</summary>
        public decimal Subtotal { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PedidoDetalleDTO"/> con todos sus campos.
        /// </summary>
        public PedidoDetalleDTO(int pedidoID, string numeroPedido, ProveedorDTO proveedor,
            EstadoPedidoDTO estado, DateTime fechaPedido, DateTime? fechaEntregaPrevista,
            DateTime? fechaRecepcion, string? observaciones, bool activo,
            List<DetallePedidoDTO> detalles, decimal subtotal)
        {
            PedidoID = pedidoID;
            NumeroPedido = numeroPedido;
            Proveedor = proveedor;
            Estado = estado;
            FechaPedido = fechaPedido;
            FechaEntregaPrevista = fechaEntregaPrevista;
            FechaRecepcion = fechaRecepcion;
            Observaciones = observaciones;
            Activo = activo;
            Detalles = detalles;
            Subtotal = subtotal;
        }
    }
}