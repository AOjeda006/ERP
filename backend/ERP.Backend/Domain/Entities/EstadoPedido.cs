using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un estado posible de un pedido
    /// </summary>
    public class EstadoPedido
    {
        #region atributos
        private int _estadoID;
        private string _nombreEstado;
        private string? _descripcion;
        private int _ordenEstado;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public EstadoPedido() { }

        /// <summary>
        /// Crea un estado de pedido con sus valores iniciales.
        /// </summary>
        /// <param name="estadoID">Identificador único del estado.</param>
        /// <param name="nombreEstado">Nombre del estado.</param>
        /// <param name="descripcion">Descripción opcional del estado.</param>
        /// <param name="ordenEstado">Número de orden del estado.</param>
        public EstadoPedido(int estadoID, string nombreEstado, string? descripcion, int ordenEstado)
        {
            _estadoID = estadoID;
            _nombreEstado = nombreEstado;
            _descripcion = descripcion;
            _ordenEstado = ordenEstado;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único del estado (clave primaria).</summary>
        [Key]
        public int EstadoID { get => _estadoID; set => _estadoID = value; }

        /// <summary>Nombre único del estado. Máximo 50 caracteres.</summary>
        [Required]
        [StringLength(50)]
        public string NombreEstado { get => _nombreEstado; set => _nombreEstado = value; }

        /// <summary>Descripción del estado. Máximo 255 caracteres. Puede ser nula.</summary>
        [StringLength(255)]
        public string? Descripcion { get => _descripcion; set => _descripcion = value; }

        /// <summary>Número de orden para representar la secuencia lógica de estados.</summary>
        [Required]
        public int OrdenEstado { get => _ordenEstado; set => _ordenEstado = value; }


        /// <summary>Colección de pedidos que se encuentran actualmente en este estado.</summary>
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        #endregion
    }
}