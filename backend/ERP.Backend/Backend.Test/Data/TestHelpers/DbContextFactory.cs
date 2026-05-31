using Data.DataSources;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.TestHelper
{
    public static class DbContextFactory
    {
        // Crea un DbContext limpio con InMemoryDatabase
        public static ApplicationDbContext CreateDbContext(string dbName = null)
        {
            dbName ??= Guid.NewGuid().ToString(); // Base única por defecto
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();  // Limpia cualquier DB anterior
            context.Database.EnsureCreated();  // Crea la DB limpia

            return context;
        }

        // Método opcional para llenar datos de prueba
        public static ApplicationDbContext CreateDbContextWithSeed(string dbName = null)
        {
            var context = CreateDbContext(dbName);

            // Ejemplo: agregar categorías y productos de prueba
            context.CategoriasProducto.AddRange(
                new Domain.Entities.CategoriaProducto { CategoriaID = 1, NombreCategoria = "Electrónica" },
                new Domain.Entities.CategoriaProducto { CategoriaID = 2, NombreCategoria = "Papelería" }
            );

            context.Productos.AddRange(
                new Domain.Entities.Producto
                {
                    ProductoID = 1,
                    NombreProducto = "Laptop",
                    CodigoProducto = "LP001",
                    CategoriaID = 1,
                    UnidadMedida = "Unidad",
                    PrecioUnitario = 1200m
                },
                new Domain.Entities.Producto
                {
                    ProductoID = 2,
                    NombreProducto = "Cuaderno",
                    CodigoProducto = "CU001",
                    CategoriaID = 2,
                    UnidadMedida = "Unidad",
                    PrecioUnitario = 3.5m
                }
            );

            context.SaveChanges();
            return context;
        }
    }
}