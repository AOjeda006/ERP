using Data.Repositories;
using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Data
{
    public class EstadoPedidoRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnEstadosOrdered()
        {
            var context = CreateDbContext("EstadoPedidoDb");
            context.EstadosPedido.AddRange(
                new EstadoPedido { EstadoID = 2, NombreEstado = "Entregado", OrdenEstado = 2 },
                new EstadoPedido { EstadoID = 1, NombreEstado = "Pendiente", OrdenEstado = 1 }
            );
            await context.SaveChangesAsync();

            var repo = new EstadoPedidoRepository(context);
            var result = await repo.GetAllAsync();

            result[0].NombreEstado.Should().Be("Pendiente");
            result[1].NombreEstado.Should().Be("Entregado");
        }
    }
}