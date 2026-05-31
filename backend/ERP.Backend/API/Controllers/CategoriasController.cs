using Application.DTOs;
using Application.UseCases;
using Domain.DTOs;
using Domain.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para la gestión de categorías de productos.
    /// Expone endpoints de solo lectura sobre el recurso <c>Categorias</c>.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaUseCases _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CategoriasController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de categorías inyectado por el contenedor DI.</param>
        public CategoriasController(ICategoriaUseCases useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene la lista completa de categorías de productos.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="CategoriaDTO"/>;
        /// 500 con el detalle del error si ocurre una excepción.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> GetAll()
        {
            ActionResult<List<CategoriaDTO>> resultado;
            try
            {
                List<CategoriaDTO> categorias = await _useCase.GetAllAsync();
                resultado = Ok(categorias);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message, inner = ex.InnerException?.Message, stack = ex.StackTrace });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene una categoría de producto por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la categoría.</param>
        /// <returns>
        /// 200 con el <see cref="CategoriaDTO"/> encontrado;
        /// 404 si no existe; 500 ante error interno.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            ActionResult<CategoriaDTO> resultado;
            try
            {
                CategoriaDTO? categoria = await _useCase.GetByIdAsync(id);
                resultado = categoria is null ? NotFound() : Ok(categoria);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}
