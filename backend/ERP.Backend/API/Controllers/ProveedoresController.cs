using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para la consulta del maestro de proveedores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorUseCase _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProveedoresController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de proveedores inyectado por el contenedor DI.</param>
        public ProveedoresController(IProveedorUseCase useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene la lista completa de proveedores del sistema.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="ProveedorDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<ProveedorDTO>>> GetAll()
        {
            ActionResult<List<ProveedorDTO>> resultado;
            try
            {
                List<ProveedorDTO> proveedores = await _useCase.GetAllAsync();
                resultado = Ok(proveedores);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un proveedor por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del proveedor.</param>
        /// <returns>
        /// 200 con el <see cref="ProveedorDTO"/>;
        /// 404 si no existe; 500 ante error interno.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDTO>> GetById(int id)
        {
            ActionResult<ProveedorDTO> resultado;
            try
            {
                ProveedorDTO? proveedor = await _useCase.GetByIdAsync(id);
                resultado = proveedor is null ? NotFound() : Ok(proveedor);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}