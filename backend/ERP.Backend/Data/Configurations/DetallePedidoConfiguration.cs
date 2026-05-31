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
    /// Configuración de Fluent API para la entidad <see cref="DetallePedido"/>.
    /// Mapea la entidad a la tabla <c>DetallesPedido</c>, define la columna calculada
    /// <c>ImporteLinea</c> y establece la relación con <see cref="Producto"/>.
    /// </summary>
    /// <remarks>
    /// La columna <c>ImporteLinea</c> se calcula en BD como:
    /// <c>[Cantidad] * [PrecioUnitario] * (1 - [Descuento] / 100)</c> (columna almacenada).
    /// Índices: <c>IDX_DetallesPedido_Pedido</c> y <c>IDX_DetallesPedido_Activo</c>.
    /// </remarks>
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<DetallePedido> builder)
        {
            builder.ToTable("DetallesPedido");

            builder.HasKey(d => d.DetallePedidoID);


            builder.Property(d => d.DetallePedidoID)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.PedidoID)
                .IsRequired();

            builder.Property(d => d.ProductoID)
                .IsRequired();

            builder.Property(d => d.Cantidad)
                .IsRequired();

            builder.Property(d => d.PrecioUnitario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(d => d.Descuento)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0m);

            builder.Property(d => d.ImporteLinea)
                .HasComputedColumnSql("[Cantidad] * [PrecioUnitario] * (1 - [Descuento] / 100)", stored: true);

            builder.Property(d => d.Activo)
                .IsRequired()
                .HasDefaultValue(true);


            builder.HasIndex(d => d.PedidoID)
                .HasDatabaseName("IDX_DetallesPedido_Pedido");

            builder.HasIndex(d => d.Activo)
                .HasDatabaseName("IDX_DetallesPedido_Activo");


            builder.HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}