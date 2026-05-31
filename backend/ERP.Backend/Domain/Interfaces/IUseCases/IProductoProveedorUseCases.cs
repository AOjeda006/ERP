using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUseCases
{
    /// <summary>
    /// Interfaz de los casos de uso de relaciones producto-proveedor.
    /// </summary>
    public interface IProductoProveedorUseCases
    {
        /// <summary>
        /// Obtiene todos los productos suministrados por un proveedor mapeados a DTO.
        /// </summary>
        /// <param name="proveedorID">Identificador del proveedor por el que filtrar.</param>
        /// <returns>Lista de <see cref="ProductoProveedorDTO"/> con los datos del producto incluidos.</returns>
        Task<List<ProductoProveedorDTO>> GetByProveedorAsync(int proveedorID);

        /// <summary>
        /// Obtiene todos los proveedores que suministran un producto concreto mapeados a DTO.
        /// </summary>
        /// <param name="productoID">Identificador del producto por el que filtrar.</param>
        /// <returns>Lista de <see cref="ProductoProveedorDTO"/> con los datos del producto y proveedor incluidos.</returns>
        Task<List<ProductoProveedorDTO>> GetByProductoAsync(int productoID);
    }
}
