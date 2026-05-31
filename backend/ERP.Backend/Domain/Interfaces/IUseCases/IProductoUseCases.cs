using Application.DTOs;
using Domain.DTOs;
using Domain.Entities;

namespace Application.UseCases
{
    /// <summary>
    /// Interfaz de los casos de uso del catálogo de productos.
    /// </summary>
    public interface IProductoUseCase
    {
        /// <summary>
        /// Obtiene todos los productos del catálogo mapeados a DTO.
        /// </summary>
        /// <returns>Lista de <see cref="ProductoDTO"/> con todos los productos y su categoría.</returns>
        Task<List<ProductoDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene un producto por su identificador único mapeado a DTO.
        /// </summary>
        /// <param name="productoID">Identificador único del producto a buscar.</param>
        /// <returns>El <see cref="ProductoDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<ProductoDTO?> GetByIdAsync(int productoID);

        /// <summary>
        /// Obtiene todos los productos pertenecientes a una categoría mapeados a DTO.
        /// </summary>
        /// <param name="categoriaID">Identificador de la categoría por la que filtrar.</param>
        /// <returns>Lista de <see cref="ProductoDTO"/> de la categoría indicada.</returns>
        Task<List<ProductoDTO>> GetByCategoriaAsync(int categoriaID);
    }
}