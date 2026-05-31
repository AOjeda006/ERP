using Domain.Entities;
using Domain.Mappers;
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class ProductoMapperTests
    {
        private readonly ProductoMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllPropertiesIncludingCategoriaNombre()
        {
            var entity = new Producto
            {
                ProductoID = 10,
                CategoriaID = 2,
                CodigoProducto = "P001",
                NombreProducto = "Manzana",
                Descripcion = "Roja",
                UnidadMedida = "Kg",
                PrecioUnitario = 5.5m,
                Categoria = new CategoriaProducto { NombreCategoria = "Frutas" }
            };

            var dto = _mapper.ToDTO(entity);

            dto.ProductoID.Should().Be(entity.ProductoID);
            dto.CategoriaNombre.Should().Be(entity.Categoria.NombreCategoria);
            dto.CodigoProducto.Should().Be(entity.CodigoProducto);
            dto.NombreProducto.Should().Be(entity.NombreProducto);
            dto.Descripcion.Should().Be(entity.Descripcion);
            dto.UnidadMedida.Should().Be(entity.UnidadMedida);
            dto.PrecioUnitario.Should().Be(entity.PrecioUnitario);
        }
    }
}