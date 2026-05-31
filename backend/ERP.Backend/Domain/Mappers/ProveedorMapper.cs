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
    /// Implementación del mapper de <see cref="Proveedor"/> a <see cref="ProveedorDTO"/>.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    public class ProveedorMapper : IProveedorMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea todos los campos del proveedor incluyendo los opcionales
        /// (<c>NombreComercial</c>, <c>Direccion</c>, <c>Ciudad</c>, <c>Provincia</c>,
        /// <c>Telefono</c>, <c>Email</c> y <c>PersonaContacto</c>), que se pasan
        /// directamente sin null-coalescing ya que el DTO los admite como nulos.
        /// </remarks>
        public ProveedorDTO ToDTO(Proveedor entity)
        {
            return new ProveedorDTO(
                entity.ProveedorID,
                entity.CIF,
                entity.RazonSocial,
                entity.NombreComercial,
                entity.Direccion,
                entity.Ciudad,
                entity.Provincia,
                entity.Telefono,
                entity.Email,
                entity.PersonaContacto
            );
        }
    }
}
