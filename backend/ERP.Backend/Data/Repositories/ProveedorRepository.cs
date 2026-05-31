using Data.DataSources;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Implementación del repositorio del maestro de proveedores.
    /// Accede a la tabla <c>Proveedores</c> a través de <see cref="ApplicationDbContext"/>.
    /// </summary>
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProveedorRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public ProveedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los proveedores del sistema sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="Proveedor"/>.</returns>
        public async Task<List<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Obtiene un proveedor por su identificador único sin seguimiento de cambios.
        /// </summary>
        /// <param name="id">Identificador del proveedor.</param>
        /// <returns>El <see cref="Proveedor"/> encontrado, o <c>null</c> si no existe.</returns>
        public async Task<Proveedor?> GetByIdAsync(int id)
        {
            return await _context.Proveedores.AsNoTracking().FirstOrDefaultAsync(p => p.ProveedorID == id);
        }
    }
}