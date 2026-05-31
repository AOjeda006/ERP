using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de categorías de producto.
    /// Define las operaciones de lectura sobre <see cref="CategoriaProducto"/>.
    /// </summary>
    public interface ICategoriaRepository
    {
        /// <summary>
        /// Obtiene todas las categorías de producto.
        /// </summary>
        /// <returns>Lista de <see cref="CategoriaProducto"/> con todos los registros.</returns>
        Task<List<CategoriaProducto>> GetAllAsync();

        /// <summary>
        /// Obtiene una categoría por su identificador único.
        /// </summary>
        /// <param name="categoriaID">Identificador único de la categoría a buscar.</param>
        /// <returns>La <see cref="CategoriaProducto"/> encontrada, o <c>null</c> si no existe.</returns>
        Task<CategoriaProducto?> GetByIdAsync(int categoriaID);
    }
}