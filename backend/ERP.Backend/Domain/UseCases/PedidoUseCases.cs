using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;

namespace Application.UseCases
{
    /// <summary>
    /// Implementación de los casos de uso de pedidos a proveedores.
    /// Orquesta las operaciones entre el repositorio y el mapper, encapsulando
    /// la lógica de negocio y el manejo de errores del ciclo de vida de un pedido.
    /// </summary>
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _repository;
        private readonly IPedidoMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PedidoUseCase"/>.
        /// </summary>
        /// <param name="repository">Repositorio de pedidos inyectado.</param>
        /// <param name="mapper">Mapper de pedidos inyectado.</param>
        public PedidoUseCase(IPedidoRepository repository, IPedidoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<PedidoDTO>> GetAllActivosAsync()
        {
            try
            {
                List<Pedido> pedidos = await _repository.GetAllActivosAsync();
                return pedidos.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos activos", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<PedidoDetalleDTO?> GetByIdAsync(int id)
        {
            try
            {
                Pedido? pedido = await _repository.GetByIdAsync(id);
                return pedido is null ? null : _mapper.ToDetalleDTO(pedido);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el pedido {id}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<PedidoDTO>> GetByProveedorAsync(int proveedorId)
        {
            try
            {
                List<Pedido> pedidos = await _repository.GetByProveedorAsync(proveedorId);
                return pedidos.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los pedidos del proveedor {proveedorId}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<PedidoDTO>> GetByEstadoAsync(int estadoId)
        {
            try
            {
                List<Pedido> pedidos = await _repository.GetByEstadoAsync(estadoId);
                return pedidos.Select(_mapper.ToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los pedidos con estado {estadoId}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al consultar el repositorio.</exception>
        public async Task<List<PedidoDetalleDTO>> GetRecibidosAsync()
        {
            try
            {
                List<Pedido> pedidos = await _repository.GetRecibidosAsync();
                return pedidos.Select(_mapper.ToDetalleDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pedidos recibidos", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al persistir el pedido.</exception>
        public async Task<PedidoDTO> CreateAsync(Pedido pedido)
        {
            try
            {
                Pedido creado = await _repository.CreateAsync(pedido);
                return _mapper.ToDTO(creado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el pedido", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al actualizar el pedido.</exception>
        public async Task UpdateAsync(Pedido pedido)
        {
            try
            {
                await _repository.UpdateAsync(pedido);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el pedido {pedido.PedidoID}", ex);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier error producido al desactivar el pedido.</exception>
        public async Task SoftDeleteAsync(int id)
        {
            try
            {
                await _repository.SoftDeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el pedido {id}", ex);
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Recupera el pedido, actualiza su <c>EstadoID</c> y persiste el cambio mediante el repositorio.
        /// </remarks>
        /// <exception cref="KeyNotFoundException">Se lanza si no existe ningún pedido con el <paramref name="pedidoId"/> indicado.</exception>
        /// <exception cref="Exception">Envuelve con un mensaje contextual cualquier otro error producido durante la operación.</exception>
        public async Task CambiarEstadoAsync(int pedidoId, int nuevoEstadoId)
        {
            try
            {
                Pedido? pedido = await _repository.GetByIdAsync(pedidoId)
                    ?? throw new KeyNotFoundException($"No se encontró el pedido {pedidoId}");

                pedido.EstadoID = nuevoEstadoId;
                await _repository.UpdateAsync(pedido);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cambiar el estado del pedido {pedidoId}", ex);
            }
        }
    }
}