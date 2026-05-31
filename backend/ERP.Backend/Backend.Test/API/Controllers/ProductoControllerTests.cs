using API.Controllers;
using Application.DTOs;              // ProductoDTO → Application.DTOs (no Domain.DTOs)
using Application.UseCases;
using Domain.Interfaces.IUseCases;   // IProductoUseCase — faltaba este using
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class ProductoControllerTests
    {
        private readonly Mock<IProductoUseCase> _mockUseCase;
        private readonly ProductosController _controller;

        public ProductoControllerTests()
        {
            _mockUseCase = new Mock<IProductoUseCase>();
            _controller = new ProductosController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfProductos()
        {
            // ProductoDTO NO tiene setters — constructor(id, codigo, nombre, desc, unidad, precio, categoriaNombre)
            var productos = new List<ProductoDTO>
            {
                new ProductoDTO(1, "P001", "Laptop",   null, "UND", 999m, "Electrónica"),
                new ProductoDTO(2, "P002", "Cuaderno", null, "UND", 1.5m, "Papelería")
            };
            _mockUseCase.Setup(x => x.GetAllAsync()).ReturnsAsync(productos);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductoDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsProducto_WhenFound()
        {
            var producto = new ProductoDTO(1, "P001", "Laptop", null, "UND", 999m, "Electrónica");
            _mockUseCase.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(producto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductoDTO>(okResult.Value);
            Assert.Equal("Laptop", returnValue.NombreProducto);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _mockUseCase.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((ProductoDTO?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}