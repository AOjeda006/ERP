using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Modelo de solicitud para definir una línea de detalle al crear un nuevo pedido.
    /// </summary>
    public class CrearDetalleRequest
    {
        /// <summary>Identificador del producto que se incluye en esta línea.</summary>
        [Required]
        public int ProductoID { get; set; }

        /// <summary>Cantidad de unidades a pedir. Debe ser mayor que 0.</summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Cantidad { get; set; }

        /// <summary>Precio unitario acordado para esta línea. Debe ser mayor que 0.</summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que 0")]
        public decimal PrecioUnitario { get; set; }

        /// <summary>Porcentaje de descuento aplicado a la línea. Valor entre 0 y 100. Por defecto 0.</summary>
        [Range(0, 100, ErrorMessage = "El descuento debe estar entre 0 y 100")]
        public decimal Descuento { get; set; } = 0;
    }
}
