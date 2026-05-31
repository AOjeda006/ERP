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
    /// Interfaz para el mapper de <see cref="EstadoPedido"/> a <see cref="EstadoPedidoDTO"/>.
    /// </summary>
    public interface IEstadoPedidoMapper
    {
        /// <summary>
        /// Convierte una entidad <see cref="EstadoPedido"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Entidad de dominio a convertir.</param>
        /// <returns>Un <see cref="EstadoPedidoDTO"/> con los datos de la entidad.</returns>
        EstadoPedidoDTO ToDTO(EstadoPedido entity);
    }
}
