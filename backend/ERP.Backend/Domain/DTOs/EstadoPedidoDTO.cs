namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para un estado de pedido.
    /// </summary>
    public class EstadoPedidoDTO
    {
        /// <summary>Identificador único del estado.</summary>
        public int EstadoID { get; }

        /// <summary>Nombre del estado.</summary>
        public string NombreEstado { get; }

        /// <summary>Descripción extendida del estado. Puede ser nula.</summary>
        public string? Descripcion { get; }

        /// <summary>Número de orden para mostrar los estados de forma secuencial.</summary>
        public int OrdenEstado { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EstadoPedidoDTO"/>.
        /// </summary>
        public EstadoPedidoDTO(int estadoID, string nombreEstado, string? descripcion, int ordenEstado)
        {
            EstadoID = estadoID;
            NombreEstado = nombreEstado;
            Descripcion = descripcion;
            OrdenEstado = ordenEstado;
        }
    }
}