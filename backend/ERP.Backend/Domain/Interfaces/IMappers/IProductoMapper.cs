using Application.DTOs;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IMappers
{
    /// <summary>
    /// Interfaz para el mapper de <see cref="Producto"/> a <see cref="ProductoDTO"/>.
    /// </summary>
    public interface IProductoMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="Producto"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Entidad de dominio a convertir.</param>
        /// <returns>Un <see cref="ProductoDTO"/> con los datos del producto.</returns>
        ProductoDTO ToDTO(Producto entity);
    }
}