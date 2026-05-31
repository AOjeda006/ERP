using Data.Repositories;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Tests.Data
{
    public class PedidoRepositoryTests : DataTestBase
    {
        [Fact]
        public async Task CreateAsync_ShouldAddPedido()
        {
            var context = CreateDbContext("PedidoDb");
            var repo = new PedidoRepository(context);

            var pedido = new Pedido
            {
                PedidoID = 1,
                NumeroPedido = "PED-001",
                ProveedorID = 1,
                EstadoID = 1,
                FechaPedido = DateTime.UtcNow,
                Activo = true,
                Detalles = new List<DetallePedido>
                {
                    new DetallePedido { DetallePedidoID = 1, ProductoID = 1, Cantidad = 5, PrecioUnitario = 10 }
                }
            };

            var result = await repo.CreateAsync(pedido);
            result.Should().NotBeNull();
            (await context.Pedidos.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldSetActivoFalse()
        {
            var context = CreateDbContext("PedidoDb2");
            var pedido = new Pedido { PedidoID = 1, NumeroPedido = "PED-001", Activo = true };
            context.Pedidos.Add(pedido);
            await context.SaveChangesAsync();

            var repo = new PedidoRepository(context);
            await repo.SoftDeleteAsync(1);

            var dbPedido = await context.Pedidos.IgnoreQueryFilters().FirstAsync();
            dbPedido.Activo.Should().BeFalse();
        }
    }
}