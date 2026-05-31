using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa una categoría de productos del catálogo.
    /// </summary>
    public class CategoriaProducto
    {
        #region atributos
        private int _categoriaID;
        private string _nombreCategoria;
        private string? _descripcion;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM. No usar directamente en lógica de dominio.
        /// </summary>
        public CategoriaProducto() { }

        /// <summary>
        /// Crea una categoría de producto con sus valores iniciales.
        /// </summary>
        /// <param name="categoriaID">Identificador único de la categoría.</param>
        /// <param name="nombreCategoria">Nombre de la categoría.</param>
        /// <param name="descripcion">Descripción opcional de la categoría.</param>
        public CategoriaProducto(int categoriaID, string nombreCategoria, string? descripcion)
        {
            _categoriaID = categoriaID;
            _nombreCategoria = nombreCategoria;
            _descripcion = descripcion;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único de la categoría (clave primaria).</summary>
        [Key]
        public int CategoriaID { get => _categoriaID; set => _categoriaID = value; }

        /// <summary>Nombre único de la categoría. Obligatorio. Máximo 100 caracteres.</summary>
        [Required]
        [StringLength(100)]
        public string NombreCategoria { get => _nombreCategoria; set => _nombreCategoria = value; }

        /// <summary>Descripción opcional de la categoría. Máximo 500 caracteres.</summary>
        [StringLength(500)]
        public string? Descripcion { get => _descripcion; set => _descripcion = value; }


        /// <summary>Colección de productos pertenecientes a esta categoría.</summary>
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
        #endregion
    }
}