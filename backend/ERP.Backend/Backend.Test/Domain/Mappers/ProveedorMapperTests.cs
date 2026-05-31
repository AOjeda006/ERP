using Domain.Entities;
using Domain.Mappers;
using FluentAssertions;

namespace Backend.Tests.Domain.Mappers
{
    public class ProveedorMapperTests
    {
        private readonly ProveedorMapper _mapper = new();

        [Fact]
        public void ToDTO_ShouldMapAllProperties()
        {
            var entity = new Proveedor
            {
                ProveedorID = 1,
                CIF = "A12345678",
                RazonSocial = "Proveedor SA",
                NombreComercial = "ProvSA",
                Telefono = "123456789",
                Email = "prov@example.com",
                PersonaContacto = "Juan"
            };

            var dto = _mapper.ToDTO(entity);

            dto.ProveedorID.Should().Be(entity.ProveedorID);
            dto.CIF.Should().Be(entity.CIF);
            dto.RazonSocial.Should().Be(entity.RazonSocial);
            dto.NombreComercial.Should().Be(entity.NombreComercial);
            dto.Telefono.Should().Be(entity.Telefono);
            dto.Email.Should().Be(entity.Email);
            dto.PersonaContacto.Should().Be(entity.PersonaContacto);
        }
    }
}