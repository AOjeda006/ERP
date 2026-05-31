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
    /// Configuración de Fluent API para la entidad <see cref="Pedido"/>.
    /// Mapea la entidad a la tabla <c>Pedidos</c>, define relaciones con <see cref="Proveedor"/>
    /// y <see cref="EstadoPedido"/>, y configura el cascade delete hacia <see cref="DetallePedido"/>.
    /// </summary>
    /// <remarks>
    /// La fecha de pedido utiliza <c>GETDATE()</c> como valor por defecto en SQL Server.
    /// Índices: <c>UQ_Pedidos_NumeroPedido</c>, <c>IDX_Pedidos_Proveedor</c>,
    /// <c>IDX_Pedidos_Estado</c>, <c>IDX_Pedidos_FechaPedido</c>, <c>IDX_Pedidos_Activo</c>.
    /// </remarks>
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.PedidoID);


            builder.Property(p => p.PedidoID)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.NumeroPedido)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ProveedorID)
                .IsRequired();

            builder.Property(p => p.EstadoID)
                .IsRequired();

            builder.Property(p => p.FechaPedido)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.FechaEntregaPrevista)
                .HasColumnType("date");

            builder.Property(p => p.FechaRecepcion)
                .HasColumnType("date");

            builder.Property(p => p.Observaciones)
                .HasMaxLength(1000);

            builder.Property(p => p.Activo)
                .IsRequired()
                .HasDefaultValue(true);


            builder.HasIndex(p => p.NumeroPedido)
                .IsUnique()
                .HasDatabaseName("UQ_Pedidos_NumeroPedido");

            builder.HasIndex(p => p.ProveedorID)
                .HasDatabaseName("IDX_Pedidos_Proveedor");

            builder.HasIndex(p => p.EstadoID)
                .HasDatabaseName("IDX_Pedidos_Estado");

            builder.HasIndex(p => p.FechaPedido)
                .HasDatabaseName("IDX_Pedidos_FechaPedido");

            builder.HasIndex(p => p.Activo)
                .HasDatabaseName("IDX_Pedidos_Activo");


            builder.HasOne(p => p.Proveedor)
                .WithMany(pr => pr.Pedidos)
                .HasForeignKey(p => p.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Estado)
                .WithMany(e => e.Pedidos)
                .HasForeignKey(p => p.EstadoID)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.Detalles)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}