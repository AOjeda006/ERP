using Data.Repositories;
using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Data
{
    public class ProductoProveedorRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task GetByProveedorAsync_ShouldReturnMatchingProductos()
        {
            var context = CreateDbContext("ProductoProveedorDb");
            // El repositorio hace Include(pp => pp.Producto), navegación requerida:
            // sin el Producto referenciado, EF la trata como INNER JOIN y descartaría la fila.
            context.Productos.AddRange(
                new Producto { ProductoID = 1, NombreProducto = "Manzana", CodigoProducto = "MAN01", UnidadMedida = "Kg", CategoriaID = 1 },
                new Producto { ProductoID = 2, NombreProducto = "Leche", CodigoProducto = "LEC01", UnidadMedida = "Litro", CategoriaID = 1 }
            );
            context.ProductosProveedores.AddRange(
                new ProductoProveedor { ProductoProveedorID = 1, ProductoID = 1, ProveedorID = 1, PrecioProveedor = 5m },
                new ProductoProveedor { ProductoProveedorID = 2, ProductoID = 2, ProveedorID = 2, PrecioProveedor = 8m }
            );
            await context.SaveChangesAsync();

            var repo = new ProductoProveedorRepository(context);
            var result = await repo.GetByProveedorAsync(1);

            result.Count.Should().Be(1);
            result[0].ProveedorID.Should().Be(1);
        }
    }
}