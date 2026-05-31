using API.Controllers;
using Application.DTOs;              // EstadoPedidoDTO → Application.DTOs (no Domain.DTOs)
using Domain.Interfaces.IUseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class EstadoPedidoControllerTests
    {
        private readonly Mock<IEstadoPedidoUseCases> _mockUseCase;
        private readonly EstadosPedidoController _controller;

        public EstadoPedidoControllerTests()
        {
            _mockUseCase = new Mock<IEstadoPedidoUseCases>();
            _controller = new EstadosPedidoController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsListOfEstados()
        {
            // EstadoPedidoDTO NO tiene setters — constructor(int id, string nombre, string? desc, int orden)
            var estados = new List<EstadoPedidoDTO>
            {
                new EstadoPedidoDTO(1, "Pendiente", null, 1),
                new EstadoPedidoDTO(2, "Recibido",  null, 2)
            };
            _mockUseCase.Setup(x => x.GetAllAsync()).ReturnsAsync(estados);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EstadoPedidoDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsEstado_WhenFound()
        {
            var estado = new EstadoPedidoDTO(1, "Pendiente", null, 1);
            _mockUseCase.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(estado);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EstadoPedidoDTO>(okResult.Value);
            Assert.Equal("Pendiente", returnValue.NombreEstado);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _mockUseCase.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((EstadoPedidoDTO?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}