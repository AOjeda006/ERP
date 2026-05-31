using Domain.Entities;
using Domain.Mappers;
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class EstadoPedidoMapperTests
    {
        private readonly EstadoPedidoMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllProperties()
        {
            var entity = new EstadoPedido
            {
                EstadoID = 1,
                NombreEstado = "Pendiente",
                Descripcion = "Esperando confirmación",
                OrdenEstado = 1
            };

            var dto = _mapper.ToDTO(entity);

            dto.EstadoID.Should().Be(entity.EstadoID);
            dto.NombreEstado.Should().Be(entity.NombreEstado);
            dto.Descripcion.Should().Be(entity.Descripcion);
            dto.OrdenEstado.Should().Be(entity.OrdenEstado);
        }
    }
}