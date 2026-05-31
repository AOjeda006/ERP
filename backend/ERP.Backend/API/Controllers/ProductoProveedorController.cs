using Application.DTOs;
using Application.UseCases;
using Domain.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador REST para consultar las relaciones entre productos y proveedores.
    /// Permite obtener los productos que ofrece un proveedor y los proveedores de un producto.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosProveedoresController : ControllerBase
    {
        private readonly IProductoProveedorUseCases _useCase;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductosProveedoresController"/>.
        /// </summary>
        /// <param name="useCase">Caso de uso de producto-proveedor inyectado por el contenedor DI.</param>
        public ProductosProveedoresController(IProductoProveedorUseCases useCase)
        {
            _useCase = useCase;
        }


        /// <summary>
        /// Obtiene todos los productos asociados a un proveedor concreto.
        /// </summary>
        /// <param name="proveedorId">Identificador del proveedor.</param>
        /// <returns>
        /// 200 con la lista de <see cref="ProductoProveedorDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<List<ProductoProveedorDTO>>> GetByProveedor(int proveedorId)
        {
            ActionResult<List<ProductoProveedorDTO>> resultado;
            try
            {
                List<ProductoProveedorDTO> productos = await _useCase.GetByProveedorAsync(proveedorId);
                resultado = Ok(productos);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los proveedores que suministran un producto concreto.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <returns>
        /// 200 con la lista de <see cref="ProductoProveedorDTO"/>;
        /// 500 ante error interno.
        /// </returns>
        [HttpGet("producto/{productoId}")]
        public async Task<ActionResult<List<ProductoProveedorDTO>>> GetByProducto(int productoId)
        {
            ActionResult<List<ProductoProveedorDTO>> resultado;
            try
            {
                List<ProductoProveedorDTO> proveedores = await _useCase.GetByProductoAsync(productoId);
                resultado = Ok(proveedores);
            }
            catch (Exception ex)
            {
                resultado = StatusCode(500, new { error = ex.Message });
            }
            return resultado;
        }
    }
}