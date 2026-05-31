using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso del maestro de proveedores.
    /// </summary>
    public class ProveedorUseCase : IProveedorUseCase
    {
        private readonly IProveedorRepository _repository;
        private readonly IProveedorMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProveedorUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de proveedores inyectado.</param>
        /// <param name="mapper">Mapper de proveedores inyectado.</param>
        public ProveedorUseCase(IProveedorRepository repository, IProveedorMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<ProveedorDTO>> GetAllAsync()
        {
            try
            {
                List<Proveedor> proveedores = await _repository.GetAllAsync();
                return proveedores.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los proveedores", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<ProveedorDTO?> GetByIdAsync(int id)
        {
            try
            {
                Proveedor? proveedor = await _repository.GetByIdAsync(id);
                return proveedor is null ? null : _mapper.ToDTO(proveedor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el proveedor {id}", ex);
            }
        }
    }
}