using Application.DTOs;              // ProveedorDTO → Application.DTOs
using Application.UseCases;          // ProveedorUseCase → Application.UseCases (no Domain.UseCases)
using Domain.Entities;
using Domain.Interfaces;             // IProveedorRepository → Domain.Interfaces (no Domain.Interfaces.Repository)
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;
using FluentAssertions;
using Moq;

namespace Backend.Tests.Domain.UseCases
{
    public class ProveedorUseCaseTests
    {
        private readonly Mock<IProveedorRepository> _repoMock;
        private readonly Mock<IProveedorMapper> _mapperMock;
        // Clase real: ProveedorUseCase (singular) — no ProveedorUseCases
        private readonly ProveedorUseCase _useCase;

        public ProveedorUseCaseTests()
        {
            _repoMock = new Mock<IProveedorRepository>();
            _mapperMock = new Mock<IProveedorMapper>();
            // Se instancia la clase CONCRETA, no la interfaz
            // ProveedorUseCase (singular) — no IProveedorUseCases ni ProveedorUseCases
            _useCase = new ProveedorUseCase(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnMappedDTOs()
        {
            var proveedores = new List<Proveedor>
            {
                new Proveedor { ProveedorID = 1, CIF = "A12345678", RazonSocial = "Proveedor SA" }
            };
            // ProveedorDTO tiene exactamente 7 parámetros:
            // (proveedorID, cif, razonSocial, nombreComercial, telefono, email, personaContacto)
            // NO tiene Direccion, CodigoPostal, Ciudad, Provincia, Pais, Activo, FechaAlta, etc.
            var dto = new ProveedorDTO(1, "A12345678", "Proveedor SA", null, null, null, null, null, null, null);

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(proveedores);
            _mapperMock.Setup(m => m.ToDTO(It.IsAny<Proveedor>())).Returns(dto);

            var result = await _useCase.GetAllAsync();

            result.Should().HaveCount(1);
            result[0].ProveedorID.Should().Be(1);
        }
    }
}