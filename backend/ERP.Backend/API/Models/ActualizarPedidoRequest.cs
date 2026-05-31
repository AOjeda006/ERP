using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Modelo de solicitud para actualizar los datos de cabecera y las líneas de detalle
    /// de un pedido existente.
    /// </summary>
    public class ActualizarPedidoRequest
    {
        /// <summary>Nueva fecha de entrega prevista del pedido. Opcional.</summary>
        public DateOnly? FechaEntregaPrevista { get; set; }

        /// <summary>Observaciones adicionales sobre el pedido. Máximo 1000 caracteres.</summary>
        [StringLength(1000)]
        public string? Observaciones { get; set; }

        /// <summary>
        /// Lista de líneas de detalle actualizadas del pedido.
        /// Debe contener al menos un elemento.
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "El pedido debe tener al menos una línea")]
        public List<ActualizarDetalleRequest> Detalles { get; set; } = new();
    }
}
