using Application.DTOs;              // EstadoPedidoDTO → Application.DTOs
using Application.UseCases;          // EstadoPedidoUseCase → Application.UseCases (no Domain.UseCases)
using Domain.Entities;
using Domain.Interfaces;             // IEstadoPedidoRepository → Domain.Interfaces (no Domain.Interfaces.Repository)
using Domain.Interfaces.IMappers;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class EstadoPedidoUseCaseTests
    {
        private readonly Mock<IEstadoPedidoRepository> _repoMock;
        private readonly Mock<IEstadoPedidoMapper> _mapperMock;
        // Clase real: EstadoPedidoUseCase (singular) — no EstadoPedidoUseCases
        private readonly EstadoPedidoUseCase _useCase;

        public EstadoPedidoUseCaseTests()
        {
            _repoMock = new Mock<IEstadoPedidoRepository>();
            _mapperMock = new Mock<IEstadoPedidoMapper>();
            // Constructor real: EstadoPedidoUseCase (singular)
            _useCase = new EstadoPedidoUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDTOs()
        {
            var lista = new List<EstadoPedido>
            {
                new EstadoPedido { EstadoID = 1, NombreEstado = "Pendiente", OrdenEstado = 1 }
            };
            // EstadoPedidoDTO tiene 4 parámetros: (id, nombre, descripcion, ordenEstado)
            // — NO 3 como tenía el test original
            var dto = new EstadoPedidoDTO(1, "Pendiente", null, 1);

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(lista);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<EstadoPedido>())).Returns(dto);

            var result = await _useCase.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].EstadoID.Should().Be(1);
        }
    }
}