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
    /// Implementación del mapper de la entidad <see cref="Pedido"/> a sus distintos DTOs.
    /// Registrado como Singleton en el contenedor DI.
    /// </summary>
    /// <remarks>
    /// Gestiona tres conversiones:
    /// <list type="bullet">
    ///   <item><see cref="ToDTO"/> — Vista resumida para listados.</item>
    ///   <item><see cref="ToDetalleDTO"/> — Vista completa con proveedor, estado y líneas.</item>
    ///   <item><see cref="ToDetallePedidoDTO"/> — Conversión individual de una línea de detalle.</item>
    /// </list>
    /// </remarks>
    public class PedidoMapper : IPedidoMapper
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Mapea los campos de cabecera del pedido. Los campos de navegación se resuelven
        /// con null-coalescing: <c>Proveedor?.RazonSocial ?? string.Empty</c> y
        /// <c>Estado?.NombreEstado ?? string.Empty</c>.
        /// <c>NumeroLineas</c> se calcula contando únicamente los detalles con <c>Activo = true</c>.
        /// <c>Subtotal</c> se obtiene llamando a <see cref="Pedido.CalcularSubtotal"/>.
        /// <c>FechaEntregaPrevista</c> se convierte de <c>DateOnly?</c> a <c>DateTime?</c>
        /// usando <c>ToDateTime(TimeOnly.MinValue)</c>.
        /// </remarks>
        public PedidoDTO ToDTO(Pedido entity)
        {
            return new PedidoDTO(
                entity.PedidoID,
                entity.NumeroPedido,
                entity.ProveedorID,
                entity.Proveedor?.RazonSocial ?? string.Empty,
                entity.EstadoID,
                entity.Estado?.NombreEstado ?? string.Empty,
                entity.FechaPedido,
                entity.FechaEntregaPrevista?.ToDateTime(TimeOnly.MinValue),
                entity.Activo,
                entity.Detalles.Count(d => d.Activo),
                entity.CalcularSubtotal()
            );
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Construye inline los DTOs anidados <see cref="ProveedorDTO"/> y <see cref="EstadoPedidoDTO"/>
        /// a partir de las propiedades de navegación del pedido.  
        /// Se utilizan valores por defecto mediante null-coalescing en los campos requeridos
        /// para evitar referencias nulas, mientras que los opcionales se asignan directamente.
        /// Solo se incluyen en <c>Detalles</c> las líneas con <c>Activo = true</c>,
        /// delegando la conversión de cada línea a <see cref="ToDetallePedidoDTO"/>.
        /// <c>FechaEntregaPrevista</c> y <c>FechaRecepcion</c> se convierten de <c>DateOnly?</c>
        /// a <c>DateTime?</c> usando <c>ToDateTime(TimeOnly.MinValue)</c>.
        /// Además, se calcula el subtotal del pedido mediante <c>CalcularSubtotal()</c>.
        /// </remarks>
        public PedidoDetalleDTO ToDetalleDTO(Pedido entity)
        {
            return new PedidoDetalleDTO(
                entity.PedidoID,
                entity.NumeroPedido,
                new ProveedorDTO(
                    entity.Proveedor?.ProveedorID ?? 0,
                    entity.Proveedor?.CIF ?? string.Empty,
                    entity.Proveedor?.RazonSocial ?? string.Empty,
                    entity.Proveedor?.NombreComercial,
                    entity.Proveedor?.Direccion,
                    entity.Proveedor?.Ciudad,
                    entity.Proveedor?.Provincia,
                    entity.Proveedor?.Telefono,
                    entity.Proveedor?.Email,
                    entity.Proveedor?.PersonaContacto
                ),
                new EstadoPedidoDTO(
                    entity.Estado?.EstadoID ?? 0,
                    entity.Estado?.NombreEstado ?? string.Empty,
                    entity.Estado?.Descripcion,
                    entity.Estado?.OrdenEstado ?? 0
                ),
                entity.FechaPedido,
                entity.FechaEntregaPrevista?.ToDateTime(TimeOnly.MinValue),
                entity.FechaRecepcion?.ToDateTime(TimeOnly.MinValue),
                entity.Observaciones,
                entity.Activo,
                entity.Detalles
                    .Where(d => d.Activo)
                    .Select(ToDetallePedidoDTO)
                    .ToList(),
                entity.CalcularSubtotal()
            );
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Mapea todos los campos de la línea de detalle. Los campos del producto se resuelven
        /// con null-coalescing: <c>Producto?.NombreProducto ?? string.Empty</c> y
        /// <c>Producto?.CodigoProducto ?? string.Empty</c>.
        /// <c>ImporteLinea</c> proviene directamente de la columna calculada en base de datos.
        /// </remarks>
        public DetallePedidoDTO ToDetallePedidoDTO(DetallePedido entity)
        {
            return new DetallePedidoDTO(
                entity.DetallePedidoID,
                entity.ProductoID,
                entity.Producto?.NombreProducto ?? string.Empty,
                entity.Producto?.CodigoProducto ?? string.Empty,
                entity.Cantidad,
                entity.PrecioUnitario,
                entity.Descuento,
                entity.ImporteLinea,
                entity.Activo
            );
        }
    }
}
