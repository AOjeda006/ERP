using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Modelo de solicitud para la creación de un nuevo pedido a proveedor,
    /// incluyendo su cabecera y las líneas de detalle.
    /// </summary>
    public class CrearPedidoRequest
    {
        /// <summary>Identificador del proveedor al que se realiza el pedido.</summary>
        [Required]
        public int ProveedorID { get; set; }

        /// <summary>Fecha estimada en la que se espera recibir el pedido. Opcional.</summary>
        public DateOnly? FechaEntregaPrevista { get; set; }

        /// <summary>Observaciones o notas adicionales relativas al pedido. Máximo 1000 caracteres.</summary>
        [StringLength(1000)]
        public string? Observaciones { get; set; }

        /// <summary>
        /// Lista de líneas de producto que componen el pedido.
        /// Debe contener al menos un elemento.
        /// </summary>
        [Required]
        [MinLength(1, ErrorMessage = "El pedido debe tener al menos una línea")]
        public List<CrearDetalleRequest> Detalles { get; set; } = new();
    }
}
