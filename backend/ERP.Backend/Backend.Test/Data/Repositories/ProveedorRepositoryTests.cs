using Data.Repositories;
using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Data
{
    public class ProveedorRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProveedor()
        {
            var context = CreateDbContext("ProveedorDb");
            context.Proveedores.Add(new Proveedor { ProveedorID = 1, CIF = "B12345678", RazonSocial = "ProvSA" });
            await context.SaveChangesAsync();

            var repo = new ProveedorRepository(context);
            var result = await repo.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.RazonSocial.Should().Be("ProvSA");
        }
    }
}