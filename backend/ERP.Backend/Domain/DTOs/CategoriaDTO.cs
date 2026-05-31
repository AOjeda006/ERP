using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para una categoría de producto.
    /// Expone únicamente los campos necesarios para las respuestas de la API.
    /// </summary>
    public class CategoriaDTO
    {
        /// <summary>Identificador único de la categoría.</summary>
        public int CategoriaID { get; }

        /// <summary>Nombre descriptivo de la categoría.</summary>
        public string NombreCategoria { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CategoriaDTO"/>.
        /// </summary>
        /// <param name="categoriaID">Identificador único de la categoría.</param>
        /// <param name="nombreCategoria">Nombre de la categoría.</param>
        public CategoriaDTO(int categoriaID, string nombreCategoria)
        {
            CategoriaID = categoriaID;
            NombreCategoria = nombreCategoria;
        }
    }
}
