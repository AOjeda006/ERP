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
    /// Implementación del repositorio de relaciones entre productos y proveedores.
    /// Consulta la tabla <c>ProductosProveedores</c> a través de <see cref="ApplicationDbContext"/>.
    /// </summary>
    public class ProductoProveedorRepository : IProductoProveedorRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProductoProveedorRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public ProductoProveedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los productos asociados a un proveedor, incluyendo los datos del producto.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <param name="proveedorId">Identificador del proveedor.</param>
        /// <returns>Lista de <see cref="ProductoProveedor"/> con el producto cargado.</returns>
        public async Task<List<ProductoProveedor>> GetByProveedorAsync(int proveedorId)
        {
            return await _context.ProductosProveedores
                .Include(pp => pp.Producto)
                .Where(pp => pp.ProveedorID == proveedorId)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los proveedores que suministran un producto, incluyendo
        /// los datos del producto y del proveedor. Sin seguimiento de cambios.
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <returns>Lista de <see cref="ProductoProveedor"/> con producto y proveedor cargados.</returns>
        public async Task<List<ProductoProveedor>> GetByProductoAsync(int productoId)
        {
            return await _context.ProductosProveedores
                .Include(pp => pp.Producto)
                .Include(pp => pp.Proveedor)
                .Where(pp => pp.ProductoID == productoId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
