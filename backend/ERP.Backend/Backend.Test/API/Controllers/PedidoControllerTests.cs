using API.Controllers;
using API.Models;                     // CrearPedidoRequest, CrearDetalleRequest — faltaba este using
using Application.DTOs;              // PedidoDTO, PedidoDetalleDTO, ProveedorDTO, EstadoPedidoDTO
using Application.UseCases;
using Domain.Interfaces.IUseCases;   // IPedidoUseCase — faltaba este using
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ERP.Backend.Test.API.Controllers
{
    public class PedidoControllerTests
    {
        private readonly Mock<IPedidoUseCase> _mockUseCase;
        private readonly PedidosController _controller;

        public PedidoControllerTests()
        {
            _mockUseCase = new Mock<IPedidoUseCase>();
            _controller = new PedidosController(_mockUseCase.Object);
        }

        [Fact]
        public async Task GetAllActivos_ReturnsPedidos()
        {
            // PedidoDTO NO tiene setters — constructor con 11 parámetros
            var pedidos = new List<PedidoDTO>
            {
                new PedidoDTO(1, "PED-0001", 0, string.Empty, 0, string.Empty,
                    DateTime.UtcNow, null, true, 0, 0m)
            };
            _mockUseCase.Setup(x => x.GetAllActivosAsync()).ReturnsAsync(pedidos);

            var result = await _controller.GetAllActivos();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<PedidoDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetById_ReturnsPedidoDetalle_WhenFound()
        {
            // PedidoDetalleDTO NO tiene setters — constructor con 11 parámetros
            var proveedor = new ProveedorDTO(0, string.Empty, string.Empty, null, null, null, null, null, null, null);
            var estado = new EstadoPedidoDTO(0, string.Empty, null, 0);
            var pedidoDetalle = new PedidoDetalleDTO(
                1, "PED-0001", proveedor, estado,
                DateTime.UtcNow, null, null, null, true,
                new List<DetallePedidoDTO>(), 0m);

            _mockUseCase.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(pedidoDetalle);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PedidoDetalleDTO>(okResult.Value);
            Assert.Equal("PED-0001", returnValue.NumeroPedido);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNotFound()
        {
            _mockUseCase.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((PedidoDetalleDTO?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_CallsUseCase()
        {
            var request = new CrearPedidoRequest
            {
                ProveedorID = 1,
                Detalles = new List<CrearDetalleRequest>
                {
                    new CrearDetalleRequest { ProductoID = 1, Cantidad = 2, PrecioUnitario = 100m }
                }
            };
            var response = new PedidoDTO(1, "PED-0001", 1, string.Empty, 1, string.Empty,
                DateTime.UtcNow, null, true, 1, 200m);

            _mockUseCase.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Pedido>()))
                        .ReturnsAsync(response);

            var result = await _controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<PedidoDTO>(createdResult.Value);
            Assert.Equal("PED-0001", returnValue.NumeroPedido);
            _mockUseCase.Verify(x => x.CreateAsync(It.IsAny<Domain.Entities.Pedido>()), Times.Once);
        }
    }
}