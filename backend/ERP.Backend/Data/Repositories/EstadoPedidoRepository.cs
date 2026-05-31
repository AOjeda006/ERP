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
    /// Implementación del repositorio de estados de pedido.
    /// Accede a la tabla <c>EstadosPedido</c> a través de <see cref="ApplicationDbContext"/>.
    /// </summary>
    public class EstadoPedidoRepository : IEstadoPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="EstadoPedidoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public EstadoPedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los estados de pedido ordenados por <c>OrdenEstado</c> ascendente,
        /// sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="EstadoPedido"/> ordenada.</returns>
        public async Task<List<EstadoPedido>> GetAllAsync()
        {
            return await _context.EstadosPedido.OrderBy(e => e.OrdenEstado).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Obtiene un estado de pedido por su identificador único sin seguimiento de cambios.
        /// </summary>
        /// <param name="id">Identificador del estado.</param>
        /// <returns>El <see cref="EstadoPedido"/> encontrado, o <c>null</c> si no existe.</returns>
        public async Task<EstadoPedido?> GetByIdAsync(int id)
        {
            return await _context.EstadosPedido.AsNoTracking().FirstOrDefaultAsync(e => e.EstadoID == id);
        }
    }
}