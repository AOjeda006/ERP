using API.Controllers;               // ProductoProveedorController — faltaba este using
using Application.DTOs;              // ProductoProveedorDTO — faltaba este using
using Domain.Interfaces.IUseCases;   // IProductoProveedorUseCases — faltaba este using
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class ProductoProveedorControllerTests
    {
        private readonly Mock<IProductoProveedorUseCases> _mockUseCase;
        private readonly ProductosProveedoresController _controller;

        public ProductoProveedorControllerTests()
        {
            _mockUseCase = new Mock<IProductoProveedorUseCases>();
            _controller = new ProductosProveedoresController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetByProveedor_ReturnsList()
        {
            // ProductoProveedorDTO NO tiene setters — constructor con 8 parámetros
            var lista = new List<ProductoProveedorDTO>
            {
                new ProductoProveedorDTO(1, 1, 1, 10m, null, "Producto1", "COD-01", "UND")
            };
            _mockUseCase.Setup(x => x.GetByProveedorAsync(1)).ReturnsAsync(lista);

            var result = await _controller.GetByProveedor(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductoProveedorDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetByProducto_ReturnsList()
        {
            var lista = new List<ProductoProveedorDTO>
            {
                new ProductoProveedorDTO(1, 1, 1, 10m, null, "Producto1", "COD-01", "UND")
            };
            _mockUseCase.Setup(x => x.GetByProductoAsync(1)).ReturnsAsync(lista);

            var result = await _controller.GetByProducto(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductoProveedorDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}