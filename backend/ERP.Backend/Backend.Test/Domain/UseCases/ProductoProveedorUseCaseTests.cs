using Application.DTOs;              // ProductoProveedorDTO → Application.DTOs
using Application.UseCases;          // ProductoProveedorUseCase → Application.UseCases
using Domain.Entities;
using Domain.Interfaces;             // IProductoProveedorRepository → Domain.Interfaces
using Domain.Interfaces.IMappers;    // IProductoProveedorMapper
using Domain.Interfaces.IUseCases;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class ProductoProveedorUseCaseTests
    {
        private readonly Mock<IProductoProveedorRepository> _repoMock;
        private readonly Mock<IProductoProveedorMapper> _mapperMock;
        private readonly ProductoProveedorUseCase _useCase;

        public ProductoProveedorUseCaseTests()
        {
            _repoMock = new Mock<IProductoProveedorRepository>();
            _mapperMock = new Mock<IProductoProveedorMapper>();
            _useCase = new ProductoProveedorUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByProveedorAsync_ShouldReturnMappedDTOs()
        {
            var lista = new List<ProductoProveedor>
            {
                new ProductoProveedor { ProductoProveedorID = 1, ProductoID = 1, ProveedorID = 5, PrecioProveedor = 10m }
            };
            // ProductoProveedorDTO NO tiene setters — solo constructor con 8 parámetros
            // (id, productoID, proveedorID, precio, tiempoEntrega, nombreProducto, codigoProducto, unidadMedida)
            var dto = new ProductoProveedorDTO(1, 1, 5, 10m, null, string.Empty, string.Empty, string.Empty);

            // El repositorio NO tiene GetAllActivosAsync() — los métodos son GetByProveedorAsync / GetByProductoAsync
            _repoMock.Setup(r => r.GetByProveedorAsync(5)).ReturnsAsync(lista);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<ProductoProveedor>())).Returns(dto);

            var result = await _useCase.GetByProveedorAsync(5);

            result.Should().HaveCount(1);
            result[0].ProductoProveedorID.Should().Be(1);
        }

        [Fact]
        public async Task GetByProductoAsync_ShouldReturnMappedDTOs()
        {
            var lista = new List<ProductoProveedor>
            {
                new ProductoProveedor { ProductoProveedorID = 2, ProductoID = 10, ProveedorID = 3, PrecioProveedor = 15m }
            };
            var dto = new ProductoProveedorDTO(2, 10, 3, 15m, null, string.Empty, string.Empty, string.Empty);

            _repoMock.Setup(r => r.GetByProductoAsync(10)).ReturnsAsync(lista);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<ProductoProveedor>())).Returns(dto);

            var result = await _useCase.GetByProductoAsync(10);

            result.Should().HaveCount(1);
            result[0].ProductoProveedorID.Should().Be(2);
        }
    }
}