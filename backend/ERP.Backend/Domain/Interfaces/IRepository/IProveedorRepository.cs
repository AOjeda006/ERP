using Domain.Entities;

namespace Domain.Interfaces
{
    /// <summary>
    /// Interfaz del repositorio del maestro de proveedores.
    /// </summary>
    public interface IProveedorRepository
    {
        /// <summary>
        /// Obtiene todos los proveedores del sistema.
        /// </summary>
        /// <returns>Lista de <see cref="Proveedor"/> con todos los registros.</returns>
        Task<List<Proveedor>> GetAllAsync();

        /// <summary>
        /// Obtiene un proveedor por su identificador único.
        /// </summary>
        /// <param name="proveedorID">Identificador único del proveedor a buscar.</param>
        /// <returns>El <see cref="Proveedor"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<Proveedor?> GetByIdAsync(int proveedorID);
    }
}