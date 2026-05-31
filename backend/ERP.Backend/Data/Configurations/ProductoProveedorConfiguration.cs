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
    /// Configuración de Fluent API para la entidad <see cref="ProductoProveedor"/>.
    /// Mapea la tabla de relación <c>ProductosProveedores</c> entre <see cref="Producto"/>
    /// y <see cref="Proveedor"/>.
    /// </summary>
    /// <remarks>
    /// Índices: clave única compuesta <c>UQ_ProductosProveedores_ProductoID_ProveedorID</c>,
    /// <c>IDX_ProductosProveedores_Prov</c> e <c>IDX_ProductosProveedores_Prod</c>.
    /// </remarks>
    public class ProductoProveedorConfiguration : IEntityTypeConfiguration<ProductoProveedor>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<ProductoProveedor> builder)
        {
            builder.ToTable("ProductosProveedores");

            builder.HasKey(pp => pp.ProductoProveedorID);


            builder.Property(pp => pp.ProductoProveedorID)
                .ValueGeneratedOnAdd();

            builder.Property(pp => pp.ProductoID)
                .IsRequired();

            builder.Property(pp => pp.ProveedorID)
                .IsRequired();

            builder.Property(pp => pp.PrecioProveedor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pp => pp.TiempoEntregaDias);


            builder.HasIndex(pp => new { pp.ProductoID, pp.ProveedorID })
                .IsUnique()
                .HasDatabaseName("UQ_ProductosProveedores_ProductoID_ProveedorID");

            builder.HasIndex(pp => pp.ProveedorID)
                .HasDatabaseName("IDX_ProductosProveedores_Prov");

            builder.HasIndex(pp => pp.ProductoID)
                .HasDatabaseName("IDX_ProductosProveedores_Prod");


            builder.HasOne(pp => pp.Producto)
                .WithMany(p => p.ProductosProveedores)
                .HasForeignKey(pp => pp.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pp => pp.Proveedor)
                .WithMany(p => p.ProductosProveedores)
                .HasForeignKey(pp => pp.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}