using Application.UseCases;          // CategoriaUseCase está en Application.UseCases
using Domain.DTOs;                    // CategoriaDTO → Domain.DTOs
using Domain.Entities;               // CategoriaProducto
using Domain.Interfaces;             // ICategoriaRepository → Domain.Interfaces (no IRepository subcarpeta)
using Domain.Interfaces.IMappers;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class CategoriaUseCaseTests
    {
        private readonly Mock<ICategoriaRepository> _repoMock;
        private readonly Mock<ICategoriaMapper> _mapperMock;
        private readonly CategoriaUseCase _useCase;

        public CategoriaUseCaseTests()
        {
            _repoMock = new Mock<ICategoriaRepository>();
            _mapperMock = new Mock<ICategoriaMapper>();
            _useCase = new CategoriaUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDTOs()
        {
            // CategoriaProducto → ok, usando Domain.Entities
            var categorias = new List<CategoriaProducto>
            {
                new CategoriaProducto { CategoriaID = 1, NombreCategoria = "Test" }
            };
            // CategoriaDTO solo tiene constructor(int id, string nombre) — sin setters
            var dto = new CategoriaDTO(1, "Test");

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categorias);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<CategoriaProducto>())).Returns(dto);

            // Método real del UseCase: GetAllAsync() — no GetAllActivosAsync()
            var result = await _useCase.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].CategoriaID.Should().Be(1);
        }
    }
}