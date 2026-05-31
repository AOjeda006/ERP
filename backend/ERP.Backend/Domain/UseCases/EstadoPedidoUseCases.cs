using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso de estados de pedido.
    /// </summary>
    public class EstadoPedidoUseCase : IEstadoPedidoUseCases
    {
        private readonly IEstadoPedidoRepository _repository;
        private readonly IEstadoPedidoMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EstadoPedidoUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de estados inyectado.</param>
        /// <param name="mapper">Mapper de estados inyectado.</param>
        public EstadoPedidoUseCase(IEstadoPedidoRepository repository, IEstadoPedidoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<EstadoPedidoDTO>> GetAllAsync()
        {
            try
            {
                List<EstadoPedido> estados = await _repository.GetAllAsync();
                return estados.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los estados de pedido", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<EstadoPedidoDTO?> GetByIdAsync(int id)
        {
            try
            {
                EstadoPedido? estado = await _repository.GetByIdAsync(id);
                return estado is null ? null : _mapper.ToDTO(estado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el estado {id}", ex);
            }
        }
    }
}