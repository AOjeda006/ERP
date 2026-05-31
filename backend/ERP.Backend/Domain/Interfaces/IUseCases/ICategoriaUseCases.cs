using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUseCases
{
    /// <summary>
    /// Interfaz de los casos de uso de categorías de producto.
    /// </summary>
    public interface ICategoriaUseCases
    {
        /// <summary>
        /// Obtiene todas las categorías de producto mapeadas a DTO.
        /// </summary>
        /// <returns>Lista de <see cref="CategoriaDTO"/> con todos los registros.</returns>
        Task<List<CategoriaDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene una categoría por su identificador único mapeada a DTO.
        /// </summary>
        /// <param name="categoriaID">Identificador único de la categoría a buscar.</param>
        /// <returns>El <see cref="CategoriaDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<CategoriaDTO?> GetByIdAsync(int categoriaID);
    }
}
