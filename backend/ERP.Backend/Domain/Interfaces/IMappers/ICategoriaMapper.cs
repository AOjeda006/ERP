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
    /// Interfaz para el mapper de <see cref="CategoriaProducto"/> a <see cref="CategoriaDTO"/>.
    /// </summary>
    public interface ICategoriaMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="CategoriaProducto"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Entidad de dominio a convertir.</param>
        /// <returns>Un <see cref="CategoriaDTO"/> con los datos de la entidad.</returns>
        CategoriaDTO ToDTO(CategoriaProducto entity);
    }
}
