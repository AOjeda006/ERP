using API.Controllers;               // ProveedoresController — faltaba este using
using Application.DTOs;              // ProveedorDTO → Application.DTOs (no Domain.DTOs)
using Application.UseCases;
using Domain.Interfaces.IUseCases;   // IProveedorUseCase — faltaba este using
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class ProveedoresControllerTests
    {
        private readonly Mock<IProveedorUseCase> _mockUseCase;
        private readonly ProveedoresController _controller;

        public ProveedoresControllerTests()
        {
            _mockUseCase = new Mock<IProveedorUseCase>();
            _controller = new ProveedoresController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfProveedores()
        {
            // ProveedorDTO NO tiene setters — constructor(id, cif, razonSocial, nombreComercial, telefono, email, personaContacto)
            var proveedores = new List<ProveedorDTO>
            {
                new ProveedorDTO(1, "A12345678", "Proveedor1", null, null, null, null, null, null, null),
                new ProveedorDTO(2, "B12345678", "Proveedor2", null, null, null, null, null, null, null)
            };
            _mockUseCase.Setup(x => x.GetAllAsync()).ReturnsAsync(proveedores);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProveedorDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsProveedor_WhenFound()
        {
            var proveedor = new ProveedorDTO(1, "A12345678", "Proveedor1", null, null, null, null, null, null, null);
            _mockUseCase.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(proveedor);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProveedorDTO>(okResult.Value);
            Assert.Equal("Proveedor1", returnValue.RazonSocial);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _mockUseCase.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((ProveedorDTO?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}