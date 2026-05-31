using Data.Repositories;
using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Data
{
    public class ProductoRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task GetAllAsync_ShouldIncludeCategoria()
        {
            var context = CreateDbContext("ProductoDb");
            var categoria = new CategoriaProducto { CategoriaID = 1, NombreCategoria = "Frutas" };
            context.CategoriasProducto.Add(categoria);
            context.Productos.Add(new Producto { ProductoID = 1, NombreProducto = "Manzana", CodigoProducto = "MAN01", UnidadMedida = "Kg", Categoria = categoria, CategoriaID = categoria.CategoriaID });
            await context.SaveChangesAsync();

            var repo = new ProductoRepository(context);
            var productos = await repo.GetAllAsync();

            productos.Count.Should().Be(1);
            productos[0].Categoria.Should().NotBeNull();
            productos[0].Categoria.NombreCategoria.Should().Be("Frutas");
        }

        [Fact]
        public async Task GetByCategoriaAsync_ShouldReturnOnlyMatchingProductos()
        {
            var context = CreateDbContext("ProductoDb2");
            // El repositorio hace Include(p => p.Categoria), navegación requerida:
            // sin las categorías referenciadas, EF las trata como INNER JOIN y descartaría las filas.
            context.CategoriasProducto.AddRange(
                new CategoriaProducto { CategoriaID = 1, NombreCategoria = "Frutas" },
                new CategoriaProducto { CategoriaID = 2, NombreCategoria = "Lácteos" }
            );
            context.Productos.AddRange(
                new Producto { ProductoID = 1, NombreProducto = "Manzana", CodigoProducto = "MAN01", UnidadMedida = "Kg", CategoriaID = 1 },
                new Producto { ProductoID = 2, NombreProducto = "Leche", CodigoProducto = "LEC01", UnidadMedida = "Litro", CategoriaID = 2 }
            );
            await context.SaveChangesAsync();

            var repo = new ProductoRepository(context);
            var result = await repo.GetByCategoriaAsync(1);

            result.Count.Should().Be(1);
            result[0].NombreProducto.Should().Be("Manzana");
        }
    }
}