using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataSources
{
    /// <summary>
    /// Contexto principal de Entity Framework Core para la aplicación ERP.
    /// Expone los <see cref="DbSet{T}"/> de todas las entidades del dominio y aplica
    /// automáticamente las configuraciones Fluent API del ensamblado.
    /// </summary>
    /// <remarks>
    /// Aplica un filtro de consulta global sobre <see cref="Pedido"/> para excluir
    /// automáticamente los registros con <c>Activo = false</c> en todas las consultas.
    /// </remarks>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>Conjunto de productos del catálogo.</summary>
        public DbSet<Producto> Productos { get; set; }

        /// <summary>Conjunto de categorías de producto.</summary>
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }

        /// <summary>Conjunto de proveedores.</summary>
        public DbSet<Proveedor> Proveedores { get; set; }

        /// <summary>Conjunto de pedidos a proveedores.</summary>
        public DbSet<Pedido> Pedidos { get; set; }

        /// <summary>Conjunto de líneas de detalle de pedidos.</summary>
        public DbSet<DetallePedido> DetallesPedido { get; set; }

        /// <summary>Conjunto de estados de pedido disponibles.</summary>
        public DbSet<EstadoPedido> EstadosPedido { get; set; }

        /// <summary>Conjunto de relaciones entre productos y proveedores.</summary>
        public DbSet<ProductoProveedor> ProductosProveedores { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">Opciones de configuración del contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        /// <summary>
        /// Aplica todas las configuraciones Fluent API del ensamblado y registra
        /// el filtro global de pedidos activos.
        /// </summary>
        /// <param name="builder">Constructor del modelo de base de datos.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
            builder.Entity<Pedido>().HasQueryFilter(p => p.Activo);
        }
    }
}
