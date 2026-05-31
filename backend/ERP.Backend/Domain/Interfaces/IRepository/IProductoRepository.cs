using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio del catálogo de productos.
    /// </summary>
    public interface IProductoRepository
    {
        /// <summary>
        /// Obtiene todos los productos del catálogo con su categoría asociada.
        /// </summary>
        /// <returns>Lista de <see cref="Producto"/> con la entidad <see cref="CategoriaProducto"/> cargada.</returns>
        Task<List<Producto>> GetAllAsync();

        /// <summary>
        /// Obtiene un producto por su identificador único con su categoría asociada.
        /// </summary>
        /// <param name="productoID">Identificador único del producto a buscar.</param>
        /// <returns>El <see cref="Producto"/> encontrado con su categoría cargada, o <c>null</c> si no existe.</returns>
        Task<Producto?> GetByIdAsync(int productoID);

        /// <summary>
        /// Obtiene todos los productos pertenecientes a una categoría específica.
        /// </summary>
        /// <param name="categoriaID">Identificador de la categoría por la que filtrar.</param>
        /// <returns>Lista de <see cref="Producto"/> de la categoría indicada con su categoría cargada.</returns>
        Task<List<Producto>> GetByCategoriaAsync(int categoriaID);
    }
}