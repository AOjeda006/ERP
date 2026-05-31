using Domain.Entities;
using Domain.DTOs;
using Application.DTOs;

namespace Application.UseCases
{
    /// <summary>
    /// Interfaz de los casos de uso de pedidos a proveedores.
    /// Define toda la lógica de negocio relacionada con el ciclo de vida de un pedido.
    /// </summary>
    public interface IPedidoUseCase
    {
        /// <summary>
        /// Obtiene todos los pedidos activos en formato resumido.
        /// </summary>
        /// <returns>Lista de <see cref="PedidoDTO"/> con los pedidos activos ordenados por fecha descendente.</returns>
        Task<List<PedidoDTO>> GetAllActivosAsync();

        /// <summary>
        /// Obtiene el detalle completo de un pedido por su identificador único.
        /// </summary>
        /// <param name="pedidoID">Identificador único del pedido a buscar.</param>
        /// <returns>El <see cref="PedidoDetalleDTO"/> con todas las relaciones cargadas, o <c>null</c> si no existe.</returns>
        Task<PedidoDetalleDTO?> GetByIdAsync(int pedidoID);

        /// <summary>
        /// Obtiene los pedidos activos de un proveedor concreto en formato resumido.
        /// </summary>
        /// <param name="proveedorID">Identificador del proveedor por el que filtrar.</param>
        /// <returns>Lista de <see cref="PedidoDTO"/> del proveedor indicado.</returns>
        Task<List<PedidoDTO>> GetByProveedorAsync(int proveedorID);

        /// <summary>
        /// Obtiene los pedidos activos en un estado concreto en formato resumido.
        /// </summary>
        /// <param name="estadoID">Identificador del estado por el que filtrar.</param>
        /// <returns>Lista de <see cref="PedidoDTO"/> en el estado indicado.</returns>
        Task<List<PedidoDTO>> GetByEstadoAsync(int estadoID);

        /// <summary>
        /// Obtiene el detalle completo de todos los pedidos en estado "Recibido".
        /// </summary>
        /// <returns>Lista de <see cref="PedidoDetalleDTO"/> de los pedidos recibidos.</returns>
        Task<List<PedidoDetalleDTO>> GetRecibidosAsync();

        /// <summary>
        /// Crea un nuevo pedido y devuelve su representación DTO resumida.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> a persistir con sus líneas de detalle.</param>
        /// <returns>El <see cref="PedidoDTO"/> del pedido creado con el identificador generado.</returns>
        Task<PedidoDTO> CreateAsync(Pedido pedido);

        /// <summary>
        /// Actualiza los datos del pedido.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> con los datos actualizados.</param>
        Task UpdateAsync(Pedido pedido);

        /// <summary>
        /// Realiza un borrado lógico del pedido marcando su campo <c>Activo</c> como <c>false</c>.
        /// </summary>
        /// <param name="pedidoID">Identificador del pedido a desactivar.</param>
        Task SoftDeleteAsync(int pedidoID);

        /// <summary>
        /// Cambia el estado de un pedido al nuevo estado indicado.
        /// </summary>
        /// <param name="pedidoId">Identificador del pedido cuyo estado se desea cambiar.</param>
        /// <param name="nuevoEstadoId">Identificador del nuevo estado al que transicionar.</param>
        /// <exception cref="KeyNotFoundException">Se lanza si no existe ningún pedido con el <paramref name="pedidoId"/> indicado.</exception>
        Task CambiarEstadoAsync(int pedidoId, int nuevoEstadoId);
    }
}