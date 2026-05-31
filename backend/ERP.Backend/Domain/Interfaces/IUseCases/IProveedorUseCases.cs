using Application.DTOs;
using Domain.DTOs;
using Domain.Entities;

namespace Application.UseCases
{
    /// <summary>
    /// Interfaz de los casos de uso del maestro de proveedores.
    /// </summary>
    public interface IProveedorUseCase
    {
        /// <summary>
        /// Obtiene todos los proveedores del sistema mapeados a DTO.
        /// </summary>
        /// <returns>Lista de <see cref="ProveedorDTO"/> con todos los registros.</returns>
        Task<List<ProveedorDTO>> GetAllAsync();

        /// <summary>
        /// Obtiene un proveedor por su identificador único mapeado a DTO.
        /// </summary>
        /// <param name="proveedorID">Identificador único del proveedor a buscar.</param>
        /// <returns>El <see cref="ProveedorDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        Task<ProveedorDTO?> GetByIdAsync(int proveedorID);
    }
}