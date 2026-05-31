using Data.Repositories;
using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Data
{
    public class CategoriaRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCategoriasOrdered()
        {
            var context = CreateDbContext("CategoriaDb");
            context.CategoriasProducto.AddRange(
                new CategoriaProducto { CategoriaID = 2, NombreCategoria = "B" },
                new CategoriaProducto { CategoriaID = 1, NombreCategoria = "A" }
            );
            await context.SaveChangesAsync();

            var repo = new CategoriaRepository(context);
            var result = await repo.GetAllAsync();

            result.Count.Should().Be(2);
            result[0].NombreCategoria.Should().Be("A");
            result[1].NombreCategoria.Should().Be("B");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCategoria_WhenExists()
        {
            var context = CreateDbContext("CategoriaDb2");
            context.CategoriasProducto.Add(new CategoriaProducto { CategoriaID = 1, NombreCategoria = "Alimentos" });
            await context.SaveChangesAsync();

            var repo = new CategoriaRepository(context);
            var result = await repo.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.NombreCategoria.Should().Be("Alimentos");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var context = CreateDbContext("CategoriaDb3");
            var repo = new CategoriaRepository(context);
            var result = await repo.GetByIdAsync(999);

            result.Should().BeNull();
        }
    }
}