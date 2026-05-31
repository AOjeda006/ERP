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
    /// Configuración de Fluent API para la entidad <see cref="EstadoPedido"/>.
    /// Mapea la entidad a la tabla <c>EstadosPedido</c> y define restricciones e índices.
    /// </summary>
    /// <remarks>
    /// Índices definidos:
    /// <list type="bullet">
    ///   <item><c>UQ_EstadosPedido_NombreEstado</c> — único sobre NombreEstado.</item>
    /// </list>
    /// </remarks>
    public class EstadoPedidoConfiguration : IEntityTypeConfiguration<EstadoPedido>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<EstadoPedido> builder)
        {
            builder.ToTable("EstadosPedido");

            builder.HasKey(e => e.EstadoID);


            builder.Property(e => e.EstadoID)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NombreEstado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Descripcion)
                .HasMaxLength(255);

            builder.Property(e => e.OrdenEstado)
                .IsRequired();


            builder.HasIndex(e => e.NombreEstado)
                .IsUnique()
                .HasDatabaseName("UQ_EstadosPedido_NombreEstado");
        }
    }
}