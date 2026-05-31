using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.IMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
    /// <summary>
    /// Implementación del mapper de <see cref="ProductoProveedor"/> a <see cref="ProductoProveedorDTO"/>.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    public class ProductoProveedorMapper : IProductoProveedorMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea todos los campos de la relación junto con los datos desnormalizados del producto.
        /// Los campos del producto se resuelven con null-coalescing:
        /// <c>Producto?.NombreProducto ?? string.Empty</c>,
        /// <c>Producto?.CodigoProducto ?? string.Empty</c> y
        /// <c>Producto?.UnidadMedida ?? string.Empty</c>.
        /// Requiere que la propiedad de navegación <c>Producto</c> esté cargada
        /// mediante <c>Include</c> en la consulta del repositorio.
        /// Los datos del proveedor no se incluyen en este DTO.
        /// </remarks>
        public ProductoProveedorDTO ToDTO(ProductoProveedor entity)
        {
            return new ProductoProveedorDTO(
                entity.ProductoProveedorID,
                entity.ProductoID,
                entity.ProveedorID,
                entity.PrecioProveedor,
                entity.TiempoEntregaDias,
                entity.Producto?.NombreProducto ?? string.Empty,
                entity.Producto?.CodigoProducto ?? string.Empty,
                entity.Producto?.UnidadMedida ?? string.Empty
            );
        }
    }
}
