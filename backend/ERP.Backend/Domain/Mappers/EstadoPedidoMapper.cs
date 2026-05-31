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
    /// Implementación del mapper de <see cref="EstadoPedido"/> a <see cref="EstadoPedidoDTO"/>.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    public class EstadoPedidoMapper : IEstadoPedidoMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea todos los campos de la entidad: <c>EstadoID</c>, <c>NombreEstado</c>,
        /// <c>Descripcion</c> (admite nulo) y <c>OrdenEstado</c>.
        /// </remarks>
        public EstadoPedidoDTO ToDTO(EstadoPedido entity)
        {
            return new EstadoPedidoDTO(
                entity.EstadoID,
                entity.NombreEstado,
                entity.Descripcion,
                entity.OrdenEstado
            );
        }
    }
}
