using System.Linq;
using Domain.Entities;
using Domain.Mappers;
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class PedidoMapperTests
    {
        private readonly PedidoMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllProperties()
        {
            var pedido = new Pedido
            {
                PedidoID = 1,
                NumeroPedido = "PED-001",
                Proveedor = new Proveedor { ProveedorID = 10, RazonSocial = "ProvSA" },
                Estado = new EstadoPedido { EstadoID = 2, NombreEstado = "Recibido" },
                FechaPedido = DateTime.UtcNow,
                FechaEntregaPrevista = DateOnly.FromDateTime(DateTime.UtcNow),
                Activo = true,
                Detalles = new List<DetallePedido>
                {
                    new DetallePedido
                    {
                        DetallePedidoID = 1,
                        Cantidad = 5,
                        PrecioUnitario = 10m,
                        Descuento = 0m,
                        Activo = true,
                        Producto = new Producto { NombreProducto = "Arroz", CodigoProducto = "AR01" }
                    }
                }
            };

            var dto = _mapper.ToDetalleDTO(pedido);

            dto.PedidoID.Should().Be(pedido.PedidoID);
            dto.NumeroPedido.Should().Be(pedido.NumeroPedido);
            dto.Proveedor.RazonSocial.Should().Be(pedido.Proveedor.RazonSocial);
            dto.Estado.NombreEstado.Should().Be(pedido.Estado.NombreEstado);
            var primerDetalle = pedido.Detalles.First();
            dto.Detalles[0].ProductoNombre.Should().Be(primerDetalle.Producto!.NombreProducto);
            dto.Detalles[0].CodigoProducto.Should().Be(primerDetalle.Producto!.CodigoProducto);
        }
    }
}