using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para la consulta de productos del catálogo.
    /// Permite obtener todos los productos, uno por ID o filtrados por categoría.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoUseCase _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductosController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de productos inyectado por el contenedor DI.</param>
        public ProductosController(IProductoUseCase useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene el catálogo completo de productos junto con su categoría asociada.
        /// </summary>
        /// <returns>
        /// 200 con la lista de <see cref="ProductoDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> GetAll()
        {
            ActionResult<List<ProductoDTO>> resultado;
            try
            {
                List<ProductoDTO> productos = await _useCase.GetAllAsync();
                resultado = Ok(productos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un producto por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <returns>
        /// 200 con el <see cref="ProductoDTO"/>;
        /// 404 si no existe; 500 ante error interno.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            ActionResult<ProductoDTO> resultado;
            try
            {
                ProductoDTO? producto = await _useCase.GetByIdAsync(id);
                resultado = producto is null ? NotFound() : Ok(producto);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los productos pertenecientes a una categoría específica.
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría por la que filtrar.</param>
        /// <returns>
        /// 200 con la lista de <see cref="ProductoDTO"/> filtrada;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<List<ProductoDTO>>> GetByCategoria(int categoriaId)
        {
            ActionResult<List<ProductoDTO>> resultado;
            try
            {
                List<ProductoDTO> productos = await _useCase.GetByCategoriaAsync(categoriaId);
                resultado = Ok(productos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}