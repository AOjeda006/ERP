using Domain.Entities;
using FluentAssertions;

namespace Backend.Tests.Domain.Entities
{
    public class ProductoTests
    {
        [Fact]
        public void Producto_Properties_ShouldSetCorrectly()
        {
            var producto = new Producto
            {
                ProductoID = 1,
                NombreProducto = "Producto Test",
                PrecioUnitario = 10.5m
            };

            producto.ProductoID.Should().Be(1);
            producto.NombreProducto.Should().Be("Producto Test");
            producto.PrecioUnitario.Should().Be(10.5m);
        }
    }
}