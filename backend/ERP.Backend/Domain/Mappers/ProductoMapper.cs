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
    /// Implementación del mapper de <see cref="Producto"/> a <see cref="ProductoDTO"/>.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    public class ProductoMapper : IProductoMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea todos los campos del producto. El nombre de la categoría se resuelve
        /// con null-coalescing: <c>Categoria?.NombreCategoria ?? string.Empty</c>,
        /// por lo que requiere que la propiedad de navegación <c>Categoria</c> esté cargada
        /// mediante <c>Include</c> en la consulta del repositorio.
        /// </remarks>
        public ProductoDTO ToDTO(Producto entity)
        {
            return new ProductoDTO(
                entity.ProductoID,
                entity.CodigoProducto,
                entity.NombreProducto,
                entity.Descripcion,
                entity.UnidadMedida,
                entity.PrecioUnitario,
                entity.Categoria?.NombreCategoria ?? string.Empty
            );
        }
    }
}
