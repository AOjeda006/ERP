using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    /// <summary>
    /// Modelo de solicitud para cambiar el estado actual de un pedido.
    /// </summary>
    public class CambiarEstadoRequest
    {
        /// <summary>
        /// Identificador del nuevo estado al que se desea transicionar el pedido.
        /// </summary>
        [Required]
        public int NuevoEstadoID { get; set; }
    }
}
