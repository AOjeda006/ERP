using Domain.DTOs;
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
    /// Implementación del mapper de <see cref="CategoriaProducto"/> a <see cref="CategoriaDTO"/>.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    public class CategoriaMapper : ICategoriaMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea únicamente <c>CategoriaID</c> y <c>NombreCategoria</c>.
        /// El campo <c>Descripcion</c> se omite intencionalmente en el DTO de listado.
        /// </remarks>
        public CategoriaDTO ToDTO(CategoriaProducto entity)
        {
            return new CategoriaDTO(
                entity.CategoriaID,
                entity.NombreCategoria
            );
        }
    }
}
