using Application.DTOs;
using Domain.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces.IMappers
{
    /// <summary>
    /// Interfaz para el mapper de la entidad <see cref="Pedido"/> a sus distintos DTOs de presentación.
    /// </summary>
    public interface IPedidoMapper
    {
        /// <summary>
        /// Convierte un <see cref="Pedido"/> en un <see cref="PedidoDTO"/> resumido (sin líneas de detalle).
        /// </summary>
        /// <param name="entity">Entidad pedido a convertir.</param>
        /// <returns><see cref="PedidoDTO"/> con los datos de cabecera del pedido.</returns>
        PedidoDTO ToDTO(Pedido entity);

        /// <summary>
        /// Convierte un <see cref="Pedido"/> en un <see cref="PedidoDetalleDTO"/> completo,
        /// incluyendo proveedor, estado y líneas de detalle activas.
        /// </summary>
        /// <param name="entity">Entidad pedido a convertir.</param>
        /// <returns><see cref="PedidoDetalleDTO"/> con toda la información del pedido.</returns>
        PedidoDetalleDTO ToDetalleDTO(Pedido entity);

        /// <summary>
        /// Convierte una entidad <see cref="DetallePedido"/> en su DTO correspondiente.
        /// </summary>
        /// <param name="entity">Línea de detalle a convertir.</param>
        /// <returns><see cref="DetallePedidoDTO"/> con los datos de la línea.</returns>
        DetallePedidoDTO ToDetallePedidoDTO(DetallePedido entity);
    }
}