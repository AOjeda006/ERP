using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    /// <summary>
    /// Configuración de Fluent API para la entidad <see cref="Proveedor"/>.
    /// Mapea la entidad a la tabla <c>Proveedores</c> y define las restricciones de longitud
    /// y el índice único sobre el CIF.
    /// </summary>
    /// <remarks>
    /// Índices: <c>UQ_Proveedores_CIF</c> — único sobre CIF.
    /// </remarks>
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedores");

            builder.HasKey(p => p.ProveedorID);


            builder.Property(p => p.ProveedorID)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CIF)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.RazonSocial)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.NombreComercial)
                .HasMaxLength(255);

            builder.Property(p => p.Direccion)
                .HasMaxLength(500);

            builder.Property(p => p.Ciudad)
                .HasMaxLength(100);

            builder.Property(p => p.Provincia)
                .HasMaxLength(100);

            builder.Property(p => p.Telefono)
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasMaxLength(255);

            builder.Property(p => p.PersonaContacto)
                .HasMaxLength(255);


            builder.HasIndex(p => p.CIF)
                .IsUnique()
                .HasDatabaseName("UQ_Proveedores_CIF");
        }
    }
}