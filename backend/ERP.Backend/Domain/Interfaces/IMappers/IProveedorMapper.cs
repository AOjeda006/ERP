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
    /// Interfaz para el mapper de <see cref="Proveedor"/> a <see cref="ProveedorDTO"/>.
    /// </summary>
    public interface IProveedorMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="Proveedor"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Entidad de dominio a convertir.</param>
        /// <returns>Un <see cref="ProveedorDTO"/> con los datos del proveedor.</returns>
        ProveedorDTO ToDTO(Proveedor entity);
    }
}