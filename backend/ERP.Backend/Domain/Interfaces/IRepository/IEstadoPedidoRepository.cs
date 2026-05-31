using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de estados de pedido.
    /// Define las operaciones de lectura sobre <see cref="EstadoPedido"/>.
    /// </summary>
    public interface IEstadoPedidoRepository
    {
        /// <summary>
        /// Obtiene todos los estados de pedido.
        /// </summary>
        /// <returns>Lista de <see cref="EstadoPedido"/> con todos los registros.</returns>
        Task<List<EstadoPedido>> GetAllAsync();

        /// <summary>
        /// Obtiene un estado de pedido por su identificador único.
        /// </summary>
        /// <param name="estadoID">Identificador único del estado a buscar.</param>
        /// <returns>El <see cref="EstadoPedido"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<EstadoPedido?> GetByIdAsync(int estadoID);
    }
}