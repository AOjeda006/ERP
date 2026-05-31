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
    /// Implementación del repositorio del catálogo de productos.
    /// Accede a la tabla <c>Productos</c> a través de <see cref="ApplicationDbContext"/>,
    /// incluyendo siempre la relación con <see cref="CategoriaProducto"/>.
    /// </summary>
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los productos del catálogo con su categoría asociada.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="Producto"/> con la categoría cargada.</returns>
        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Productos.Include(p => p.Categoria).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Obtiene un producto por su identificador único con su categoría asociada.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <returns>El <see cref="Producto"/> encontrado, o <c>null</c> si no existe.</returns>
        public async Task<Producto?> GetByIdAsync(int productoId)
        {
            return await _context.Productos.Include(p => p.Categoria).AsNoTracking().FirstOrDefaultAsync(p => p.ProductoID == productoId);
        }

        /// <summary>
        /// Obtiene todos los productos de una categoría específica con su categoría cargada.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <param name="categoriaId">Identificador de la categoría por la que filtrar.</param>
        /// <returns>Lista de <see cref="Producto"/> de la categoría indicada.</returns>
        public async Task<List<Producto>> GetByCategoriaAsync(int categoriaId)
        {
            return await _context.Productos.Include(p => p.Categoria).Where(p => p.CategoriaID == categoriaId).AsNoTracking().ToListAsync();
        }
    }
}