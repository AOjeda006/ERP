using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de relaciones producto-proveedor.
    /// </summary>
    public interface IProductoProveedorRepository
    {
        /// <summary>
        /// Obtiene todos los productos suministrados por un proveedor, incluyendo los datos del producto.
        /// </summary>
        /// <param name="proveedorID">Identificador del proveedor por el que filtrar.</param>
        /// <returns>Lista de <see cref="ProductoProveedor"/> con la entidad <see cref="Producto"/> cargada.</returns>
        Task<List<ProductoProveedor>> GetByProveedorAsync(int proveedorID);

        /// <summary>
        /// Obtiene todos los proveedores que suministran un producto concreto,
        /// incluyendo los datos del producto y del proveedor.
        /// </summary>
        /// <param name="productoID">Identificador del producto por el que filtrar.</param>
        /// <returns>Lista de <see cref="ProductoProveedor"/> con las entidades <see cref="Producto"/> y <see cref="Proveedor"/> cargadas.</returns>
        Task<List<ProductoProveedor>> GetByProductoAsync(int productoID);
    }
}