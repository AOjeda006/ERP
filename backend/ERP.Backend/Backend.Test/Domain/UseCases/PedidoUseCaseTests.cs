using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class PedidoUseCaseTests
    {
        private readonly Mock<IPedidoRepository> _repoMock;
        private readonly Mock<IPedidoMapper> _mapperMock;
        private readonly IPedidoUseCase _useCase;

        public PedidoUseCaseTests()
        {
            _repoMock = new Mock<IPedidoRepository>();
            _mapperMock = new Mock<IPedidoMapper>();
            _useCase = new PedidoUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllActivosAsync_ShouldReturnMappedDTOs()
        {
            var pedidos = new List<Pedido>
            {
                new Pedido
                {
                    PedidoID     = 1,
                    NumeroPedido = "PED-001",
                    // ⚠️ Detalles debe ser una lista vacía (no null) para que
                    //    CalcularSubtotal() no lance NullReferenceException
                    //    y el mapper no intente iterar sobre null
                    Detalles = new List<DetallePedido>()
                }
            };

            var dto = new PedidoDTO(1, "PED-001", 0, string.Empty, 0, string.Empty,
                DateTime.UtcNow, null, true, 0, 0m);

            _repoMock.Setup(r => r.GetAllActivosAsync()).ReturnsAsync(pedidos);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<Pedido>())).Returns(dto);

            var result = await _useCase.GetAllActivosAsync();

            result.Should().HaveCount(1);
            result[0].PedidoID.Should().Be(1);
        }

        [Fact]
        public async Task CambiarEstadoAsync_WhenPedidoNotFound_ThrowsKeyNotFoundException()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Pedido?)null);

            Func<Task> act = async () => await _useCase.CambiarEstadoAsync(99, 2);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}