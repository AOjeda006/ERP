using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso de categorías de producto.
    /// Actúa como intermediario entre el controlador y el repositorio,
    /// aplicando el mapeo a DTO y encapsulando el manejo de errores.
    /// </summary>
    public class CategoriaUseCase : ICategoriaUseCases
    {
        private readonly ICategoriaRepository _repository;
        private readonly ICategoriaMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CategoriaUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de categorías inyectado.</param>
        /// <param name="mapper">Mapper de categorías inyectado.</param>
        public CategoriaUseCase(ICategoriaRepository repository, ICategoriaMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<CategoriaDTO>> GetAllAsync()
        {
            try
            {
                List<CategoriaProducto> categorias = await _repository.GetAllAsync();
                return categorias.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorías", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<CategoriaDTO?> GetByIdAsync(int id)
        {
            try
            {
                CategoriaProducto? categoria = await _repository.GetByIdAsync(id);
                return categoria is null ? null : _mapper.ToDTO(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la categoría {id}", ex);
            }
        }
    }
}