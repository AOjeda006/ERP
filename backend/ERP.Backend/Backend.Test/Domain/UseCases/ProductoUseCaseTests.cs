using Application.DTOs;              // ProductoDTO → Application.DTOs
using Application.UseCases;          // ProductoUseCase → Application.UseCases (no Domain.UseCases)
using Domain.Entities;
using Domain.Interfaces;             // IProductoRepository → Domain.Interfaces (no Domain.Interfaces.Repository)
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class ProductoUseCaseTests
    {
        private readonly Mock<IProductoRepository> _repoMock;
        private readonly Mock<IProductoMapper> _mapperMock;
        // Clase real: ProductoUseCase (singular) — no ProductoUseCases
        private readonly ProductoUseCase _useCase;

        public ProductoUseCaseTests()
        {
            _repoMock = new Mock<IProductoRepository>();
            _mapperMock = new Mock<IProductoMapper>();
            // Se instancia la clase CONCRETA, no la interfaz
            // ProductoUseCase (singular) — no IProductoUseCases ni ProductoUseCases
            _useCase = new ProductoUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDTOs()
        {
            var productos = new List<Producto>
            {
                new Producto { ProductoID = 1, CodigoProducto = "P01", NombreProducto = "Test",
                               UnidadMedida = "UND", PrecioUnitario = 1m }
            };
            // ProductoDTO tiene 7 parámetros: (id, codigo, nombre, descripcion, unidadMedida, precio, categoriaNombre)
            // NO tiene stockActual (param 7) ni precioUnitario nullable — esos campos no existen en el DTO real
            var dto = new ProductoDTO(1, string.Empty, string.Empty, null, string.Empty, 0m, string.Empty);

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(productos);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<Producto>())).Returns(dto);

            var result = await _useCase.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].ProductoID.Should().Be(1);
        }
    }
}