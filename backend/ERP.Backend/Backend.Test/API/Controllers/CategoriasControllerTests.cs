using API.Controllers;               // CategoriasController — faltaba este using
using Domain.DTOs;                    // CategoriaDTO → Domain.DTOs
using Domain.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class CategoriasControllerTests
    {
        private readonly Mock<ICategoriaUseCases> _mockUseCase;
        private readonly CategoriasController _controller;

        public CategoriasControllerTests()
        {
            _mockUseCase = new Mock<ICategoriaUseCases>();
            _controller = new CategoriasController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfCategorias()
        {
            // CategoriaDTO NO tiene setters — solo constructor(int id, string nombre)
            var categorias = new List<CategoriaDTO>
            {
                new CategoriaDTO(1, "Electrónica"),
                new CategoriaDTO(2, "Papelería")
            };
            _mockUseCase.Setup(x => x.GetAllAsync()).ReturnsAsync(categorias);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CategoriaDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsCategoria_WhenFound()
        {
            var categoria = new CategoriaDTO(1, "Electrónica");
            _mockUseCase.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(categoria);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CategoriaDTO>(okResult.Value);
            Assert.Equal("Electrónica", returnValue.NombreCategoria);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _mockUseCase.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((CategoriaDTO?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}