using Application.DTOs;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUseCases
{
    /// <summary>
    /// Interfaz de los casos de uso de estados de pedido.
    /// </summary>
    public interface IEstadoPedidoUseCases
    {
        /// <summary>
        /// Obtiene todos los estados de pedido mapeados a DTO.
        /// </summary>
        /// <returns>Lista de <see cref="EstadoPedidoDTO"/> ordenada por <c>OrdenEstado</c>.</returns>
        Task<List<EstadoPedidoDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene un estado de pedido por su identificador único mapeado a DTO.
        /// </summary>
        /// <param name="id">Identificador único del estado a buscar.</param>
        /// <returns>El <see cref="EstadoPedidoDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<EstadoPedidoDTO?> GetByIdAsync(int id);
    }
}
