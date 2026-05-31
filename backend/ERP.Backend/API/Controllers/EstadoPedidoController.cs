using Application.DTOs;
using Application.UseCases;
using Domain.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para la consulta de los estados de pedido disponibles en el sistema.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosPedidoController : ControllerBase
    {
        private readonly IEstadoPedidoUseCases _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EstadosPedidoController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de estados de pedido inyectado por el contenedor DI.</param>
        public EstadosPedidoController(IEstadoPedidoUseCases useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene todos los estados de pedido ordenados por su campo <c>OrdenEstado</c>.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="EstadoPedidoDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<EstadoPedidoDTO>>> GetAll()
        {
            ActionResult<List<EstadoPedidoDTO>> resultado;
            try
            {
                List<EstadoPedidoDTO> estados = await _useCase.GetAllAsync();
                resultado = Ok(estados);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un estado de pedido por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del estado de pedido.</param>
        /// <returns>
        /// 200 con el <see cref="EstadoPedidoDTO"/>;
        /// 404 si no existe; 500 ante error interno.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoPedidoDTO>> GetById(int id)
        {
            ActionResult<EstadoPedidoDTO> resultado;
            try
            {
                EstadoPedidoDTO? estado = await _useCase.GetByIdAsync(id);
                resultado = estado is null ? NotFound() : Ok(estado);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}