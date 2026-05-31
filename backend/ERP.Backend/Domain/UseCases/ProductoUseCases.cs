using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso del catálogo de productos.
    /// </summary>
    public class ProductoUseCase : IProductoUseCase
    {
        private readonly IProductoRepository _repository;
        private readonly IProductoMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de productos inyectado.</param>
        /// <param name="mapper">Mapper de productos inyectado.</param>
        public ProductoUseCase(IProductoRepository repository, IProductoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<ProductoDTO>> GetAllAsync()
        {
            try
            {
                List<Producto> productos = await _repository.GetAllAsync();
                return productos.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            try
            {
                Producto? producto = await _repository.GetByIdAsync(id);
                return producto is null ? null : _mapper.ToDTO(producto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el producto {id}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<ProductoDTO>> GetByCategoriaAsync(int categoriaId)
        {
            try
            {
                List<Producto> productos = await _repository.GetByCategoriaAsync(categoriaId);
                return productos.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los productos de la categoría {categoriaId}", ex);
            }
        }
    }
}