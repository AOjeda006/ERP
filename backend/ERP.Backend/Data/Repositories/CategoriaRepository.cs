using Data.DataSources;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Implementación del repositorio de categorías de producto.
    /// Accede a la tabla <c>CategoriasProducto</c> a través de <see cref="ApplicationDbContext"/>.
    /// </summary>
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="CategoriaRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las categorías de producto sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="CategoriaProducto"/>.</returns>
        public async Task<List<CategoriaProducto>> GetAllAsync()
        {
            return await _context.CategoriasProducto
                .OrderBy(c => c.NombreCategoria)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una categoría por su identificador único sin seguimiento de cambios.
        /// </summary>
        /// <param name="categoriaID">Identificador de la categoría.</param>
        /// <returns>La <see cref="CategoriaProducto"/> encontrada, o <c>null</c> si no existe.</returns>
        public async Task<CategoriaProducto?> GetByIdAsync(int categoriaID)
        {
            return await _context.CategoriasProducto.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaID == categoriaID);
        }
    }
}