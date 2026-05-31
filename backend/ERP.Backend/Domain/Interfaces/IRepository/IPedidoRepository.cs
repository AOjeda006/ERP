using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio de pedidos a proveedores.
    /// Define todas las operaciones de consulta y persistencia sobre <see cref="Pedido"/>.
    /// </summary>
    public interface IPedidoRepository
    {
        /// <summary>
        /// Obtiene todos los pedidos activos ordenados por fecha de pedido descendente.
        /// </summary>
        /// <returns>Lista de <see cref="Pedido"/> activos con proveedor, estado y detalles cargados.</returns>
        Task<List<Pedido>> GetAllActivosAsync();

        /// <summary>
        /// Obtiene un pedido con todo su detalle por su identificador único.
        /// </summary>
        /// <param name="pedidoID">Identificador único del pedido a buscar.</param>
        /// <returns>El <see cref="Pedido"/> encontrado con todas sus relaciones, o <c>null</c> si no existe.</returns>
        Task<Pedido?> GetByIdAsync(int pedidoID);

        /// <summary>
        /// Obtiene todos los pedidos activos de un proveedor concreto.
        /// </summary>
        /// <param name="proveedorID">Identificador del proveedor por el que filtrar.</param>
        /// <returns>Lista de <see cref="Pedido"/> activos del proveedor indicado.</returns>
        Task<List<Pedido>> GetByProveedorAsync(int proveedorID);

        /// <summary>
        /// Obtiene todos los pedidos activos en un estado concreto.
        /// </summary>
        /// <param name="estadoID">Identificador del estado por el que filtrar.</param>
        /// <returns>Lista de <see cref="Pedido"/> activos en el estado indicado.</returns>
        Task<List<Pedido>> GetByEstadoAsync(int estadoID);

        /// <summary>
        /// Obtiene todos los pedidos activos cuyo estado sea "Recibido".
        /// </summary>
        /// <returns>Lista de <see cref="Pedido"/> en estado "Recibido" con detalle de producto cargado.</returns>
        Task<List<Pedido>> GetRecibidosAsync();

        /// <summary>
        /// Crea un nuevo pedido en la base de datos junto con sus líneas de detalle.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> a insertar.</param>
        /// <returns>El <see cref="Pedido"/> insertado con el identificador generado por la base de datos.</returns>
        Task<Pedido> CreateAsync(Pedido pedido);

        /// <summary>
        /// Actualiza un pedido existente y gestiona el alta, modificación y baja de sus líneas de detalle.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> con los datos actualizados y sus líneas.</param>
        /// <exception cref="KeyNotFoundException">Se lanza si no existe ningún pedido con el <c>PedidoID</c> indicado.</exception>
        Task UpdateAsync(Pedido pedido);

        /// <summary>
        /// Realiza un borrado lógico del pedido marcando su campo <c>Activo</c> como <c>false</c>.
        /// Si el pedido no existe, la operación no tiene efecto.
        /// </summary>
        /// <param name="pedidoID">Identificador del pedido a desactivar.</param>
        Task SoftDeleteAsync(int pedidoID);
    }
}