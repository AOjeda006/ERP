using API.Models;
using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para la gestión completa del ciclo de vida de los pedidos a proveedores.
    /// Permite crear, consultar, actualizar, cambiar estado y eliminar (soft delete) pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoUseCase _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PedidosController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de pedidos inyectado por el contenedor DI.</param>
        public PedidosController(IPedidoUseCase useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene todos los pedidos activos del sistema, ordenados por fecha descendente.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="PedidoDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<PedidoDTO>>> GetAllActivos()
        {
            ActionResult<List<PedidoDTO>> resultado;
            try
            {
                List<PedidoDTO> pedidos = await _useCase.GetAllActivosAsync();
                resultado = Ok(pedidos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el detalle completo de un pedido, incluyendo sus líneas activas.
        /// </summary>
        /// <param name="id">Identificador del pedido.</param>
        /// <returns>
        /// 200 con el <see cref="PedidoDetalleDTO"/>;
        /// 404 si no existe; 500 ante error interno.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDetalleDTO>> GetById(int id)
        {
            ActionResult<PedidoDetalleDTO> resultado;
            try
            {
                PedidoDetalleDTO? pedido = await _useCase.GetByIdAsync(id);
                resultado = pedido is null ? NotFound() : Ok(pedido);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los pedidos activos asociados a un proveedor concreto.
        /// </summary>
        /// <param name="proveedorId">Identificador del proveedor por el que filtrar.</param>
        /// <returns>
        /// 200 con la lista de <see cref="PedidoDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<List<PedidoDTO>>> GetByProveedor(int proveedorId)
        {
            ActionResult<List<PedidoDTO>> resultado;
            try
            {
                List<PedidoDTO> pedidos = await _useCase.GetByProveedorAsync(proveedorId);
                resultado = Ok(pedidos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los pedidos activos que se encuentran en un estado concreto.
        /// </summary>
        /// <param name="estadoId">Identificador del estado por el que filtrar.</param>
        /// <returns>
        /// 200 con la lista de <see cref="PedidoDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("estado/{estadoId}")]
        public async Task<ActionResult<List<PedidoDTO>>> GetByEstado(int estadoId)
        {
            ActionResult<List<PedidoDTO>> resultado;
            try
            {
                List<PedidoDTO> pedidos = await _useCase.GetByEstadoAsync(estadoId);
                resultado = Ok(pedidos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el detalle completo de todos los pedidos en estado "Recibido",
        /// ordenados por fecha de recepción descendente.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="PedidoDetalleDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("recibidos")]
        public async Task<ActionResult<List<PedidoDetalleDTO>>> GetRecibidos()
        {
            ActionResult<List<PedidoDetalleDTO>> resultado;
            try
            {
                List<PedidoDetalleDTO> pedidos = await _useCase.GetRecibidosAsync();
                resultado = Ok(pedidos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Crea un nuevo pedido a partir de los datos del <paramref name="request"/>.
        /// El número de pedido se genera automáticamente con el formato <c>PED-{timestamp}-{guid}</c>
        /// y el estado inicial se asigna a 1 (Borrador).
        /// </summary>
        /// <param name="request">Datos necesarios para crear el pedido y sus líneas de detalle.</param>
        /// <returns>
        /// 201 con el <see cref="PedidoDTO"/> creado y la URL en la cabecera <c>Location</c>;
        /// 500 ante error interno.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Create([FromBody] CrearPedidoRequest request)
        {
            ActionResult<PedidoDTO> resultado;
            try
            {
                var pedido = new Pedido
                {
                    NumeroPedido = $"PED-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}",
                    ProveedorID = request.ProveedorID,
                    EstadoID = 1,
                    FechaPedido = DateTime.UtcNow,
                    FechaEntregaPrevista = request.FechaEntregaPrevista,
                    Observaciones = request.Observaciones,
                    Activo = true,
                    Detalles = request.Detalles.Select(d => new DetallePedido
                    {
                        ProductoID = d.ProductoID,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Descuento = d.Descuento,
                        Activo = true
                    }).ToList()
                };

                PedidoDTO creado = await _useCase.CreateAsync(pedido);
                resultado = CreatedAtAction(nameof(GetById), new { id = creado.PedidoID }, creado);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza la cabecera y las líneas de detalle de un pedido existente.
        /// Las líneas con <c>DetallePedidoID > 0</c> se actualizan; las que no tienen ID se insertan;
        /// las que ya no estén en la lista se eliminan físicamente.
        /// </summary>
        /// <param name="id">Identificador del pedido a actualizar.</param>
        /// <param name="request">Nuevos datos de cabecera y detalles del pedido.</param>
        /// <returns>
        /// 204 si la actualización fue exitosa;
        /// 404 si el pedido no existe; 500 ante error interno.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ActualizarPedidoRequest request)
        {
            ActionResult resultado;
            try
            {
                PedidoDetalleDTO? existente = await _useCase.GetByIdAsync(id);
                if (existente is null)
                {
                    return NotFound(new { error = $"No se encontró el pedido {id}" });
                }

                var pedido = new Pedido
                {
                    PedidoID = id,
                    NumeroPedido = existente.NumeroPedido,
                    ProveedorID = existente.Proveedor.ProveedorID,
                    EstadoID = existente.Estado.EstadoID,
                    FechaPedido = existente.FechaPedido,
                    FechaEntregaPrevista = request.FechaEntregaPrevista,
                    Observaciones = request.Observaciones,
                    Activo = existente.Activo,
                    Detalles = request.Detalles.Select(d => new DetallePedido
                    {
                        DetallePedidoID = d.DetallePedidoID ?? 0,
                        PedidoID = id,
                        ProductoID = d.ProductoID,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Descuento = d.Descuento,
                        Activo = true
                    }).ToList()
                };

                await _useCase.UpdateAsync(pedido);
                resultado = NoContent();
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Cambia el estado de un pedido existente al estado indicado en el <paramref name="request"/>.
        /// </summary>
        /// <param name="id">Identificador del pedido cuyo estado se desea cambiar.</param>
        /// <param name="request">Objeto que contiene el identificador del nuevo estado.</param>
        /// <returns>
        /// 204 si el cambio fue exitoso;
        /// 404 si el pedido no existe; 500 ante error interno.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Se propaga si el repositorio no encuentra el pedido con el <paramref name="id"/> especificado.
        /// </exception>
        [HttpPatch("{id}/estado")]
        public async Task<ActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoRequest request)
        {
            ActionResult resultado;
            try
            {
                await _useCase.CambiarEstadoAsync(id, request.NuevoEstadoID);
                resultado = NoContent();
            }
            catch (KeyNotFoundException)
            {
                resultado = NotFound(new { error = $"No se encontró el pedido {id}" });
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Realiza un borrado lógico (soft delete) del pedido, marcando su campo <c>Activo</c>
        /// como <c>false</c>. El registro se conserva en base de datos.
        /// </summary>
        /// <param name="id">Identificador del pedido a desactivar.</param>
        /// <returns>
        /// 204 si la operación fue exitosa;
        /// 500 ante error interno.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            ActionResult resultado;
            try
            {
                await _useCase.SoftDeleteAsync(id);
                resultado = NoContent();
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}
