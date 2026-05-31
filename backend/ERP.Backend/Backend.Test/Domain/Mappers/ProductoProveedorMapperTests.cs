using Domain.Entities;
using Domain.Mappers;                // ProductoProveedorMapper — faltaba este using
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class ProductoProveedorMapperTests
    {
        private readonly ProductoProveedorMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllPropertiesIncludingProducto()
        {
            var entity = new ProductoProveedor
            {
                ProductoProveedorID = 1,
                ProductoID = 10,
                ProveedorID = 5,
                PrecioProveedor = 15.5m,
                TiempoEntregaDias = 3,
                Producto = new Producto
                {
                    ProductoID = 10,
                    CategoriaID = 1,
                    NombreProducto = "Arroz",
                    CodigoProducto = "AR01",
                    UnidadMedida = "Kg",
                    PrecioUnitario = 15.5m
                }
            };

            var dto = _mapper.ToDTO(entity);

            dto.ProductoProveedorID.Should().Be(entity.ProductoProveedorID);
            dto.ProductoID.Should().Be(entity.ProductoID);
            dto.ProveedorID.Should().Be(entity.ProveedorID);
            dto.PrecioProveedor.Should().Be(entity.PrecioProveedor);
            dto.TiempoEntregaDias.Should().Be(entity.TiempoEntregaDias);
            dto.ProductoNombre.Should().Be(entity.Producto.NombreProducto);
            dto.CodigoProducto.Should().Be(entity.Producto.CodigoProducto);
            dto.UnidadMedida.Should().Be(entity.Producto.UnidadMedida);
        }
    }
}