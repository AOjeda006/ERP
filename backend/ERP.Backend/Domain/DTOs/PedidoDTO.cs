namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos resumido de un pedido.
    /// Se utiliza en los listados donde no es necesario mostrar las líneas de detalle.
    /// </summary>
    public class PedidoDTO
    {
        /// <summary>Identificador único del pedido.</summary>
        public int PedidoID { get; }

        /// <summary>Número de pedido generado automáticamente.</summary>
        public string NumeroPedido { get; }

        /// <summary>Identificador del proveedor al que se realizó el pedido.</summary>
        public int ProveedorID { get; }

        /// <summary>Razón social del proveedor.</summary>
        public string ProveedorNombre { get; }

        /// <summary>Identificador del estado actual del pedido.</summary>
        public int EstadoID { get; }

        /// <summary>Nombre del estado actual del pedido.</summary>
        public string EstadoNombre { get; }

        /// <summary>Fecha y hora de creación del pedido.</summary>
        public DateTime FechaPedido { get; }

        /// <summary>Fecha prevista de entrega. Puede ser nula.</summary>
        public DateTime? FechaEntregaPrevista { get; }

        /// <summary>Indica si el pedido está activo.</summary>
        public bool Activo { get; }

        /// <summary>Número de líneas de detalle activas del pedido.</summary>
        public int NumeroLineas { get; }

        /// <summary>Importe total calculado a partir de las líneas activas.</summary>
        public decimal Subtotal { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PedidoDTO"/> con todos sus campos.
        /// </summary>
        public PedidoDTO(int pedidoID, string numeroPedido, int proveedorID, string proveedorNombre,
            int estadoID, string estadoNombre, DateTime fechaPedido, DateTime? fechaEntregaPrevista,
            bool activo, int numeroLineas, decimal subtotal)
        {
            PedidoID = pedidoID;
            NumeroPedido = numeroPedido;
            ProveedorID = proveedorID;
            ProveedorNombre = proveedorNombre;
            EstadoID = estadoID;
            EstadoNombre = estadoNombre;
            FechaPedido = fechaPedido;
            FechaEntregaPrevista = fechaEntregaPrevista;
            Activo = activo;
            NumeroLineas = numeroLineas;
            Subtotal = subtotal;
        }
    }
}