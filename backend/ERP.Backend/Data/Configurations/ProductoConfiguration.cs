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
    /// Configuración de Fluent API para la entidad <see cref="Producto"/>.
    /// Mapea la entidad a la tabla <c>Productos</c> y define la relación con
    /// <see cref="CategoriaProducto"/>.
    /// </summary>
    /// <remarks>
    /// Índices: <c>UQ_Productos_CodigoProducto</c> e <c>IDX_Productos_Categoria</c>.
    /// </remarks>
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(p => p.ProductoID);


            builder.Property(p => p.ProductoID)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CategoriaID)
                .IsRequired();

            builder.Property(p => p.CodigoProducto)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.NombreProducto)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500);

            builder.Property(p => p.UnidadMedida)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PrecioUnitario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.HasIndex(p => p.CodigoProducto)
                .IsUnique()
                .HasDatabaseName("UQ_Productos_CodigoProducto");

            builder.HasIndex(p => p.CategoriaID)
                .HasDatabaseName("IDX_Productos_Categoria");


            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}