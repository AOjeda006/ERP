using Domain.Entities;               // CategoriaProducto — faltaba este using
using Domain.Mappers;
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class CategoriaMapperTests
    {
        private readonly CategoriaMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllProperties()
        {
            var entity = new CategoriaProducto
            {
                CategoriaID = 1,
                NombreCategoria = "Alimentos",
                Descripcion = "Categoria de alimentos"  // campo de entidad, no se mapea al DTO
            };

            var dto = _mapper.ToDTO(entity);

            dto.CategoriaID.Should().Be(entity.CategoriaID);
            dto.NombreCategoria.Should().Be(entity.NombreCategoria);
        }
    }
}