using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IMappers
{
    /// <summary>
    /// Interfaz para el mapper de <see cref="ProductoProveedor"/> a <see cref="ProductoProveedorDTO"/>.
    /// </summary>
    public interface IProductoProveedorMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="ProductoProveedor"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Entidad de dominio a convertir.</param>
        /// <returns>Un <see cref="ProductoProveedorDTO"/> con los datos de la relación.</returns>
        ProductoProveedorDTO ToDTO(ProductoProveedor entity);
    }
}
