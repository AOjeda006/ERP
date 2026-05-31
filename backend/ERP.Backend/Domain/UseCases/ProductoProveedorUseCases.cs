using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso de relaciones producto-proveedor.
    /// </summary>
    public class ProductoProveedorUseCase : IProductoProveedorUseCases
    {
        private readonly IProductoProveedorRepository _repository;
        private readonly IProductoProveedorMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoProveedorUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de producto-proveedor inyectado.</param>
        /// <param name="mapper">Mapper de producto-proveedor inyectado.</param>
        public ProductoProveedorUseCase(IProductoProveedorRepository repository, IProductoProveedorMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<ProductoProveedorDTO>> GetByProveedorAsync(int proveedorId)
        {
            try
            {
                List<ProductoProveedor> productos = await _repository.GetByProveedorAsync(proveedorId);
                return productos.Select((ProductoProveedor p) => _mapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los productos del proveedor {proveedorId}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<ProductoProveedorDTO>> GetByProductoAsync(int productoId)
        {
            try
            {
                List<ProductoProveedor> productos = await _repository.GetByProductoAsync(productoId);
                return productos.Select((ProductoProveedor p) => _mapper.ToDTO(p)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los proveedores del producto {productoId}", ex);
            }
        }
    }
}