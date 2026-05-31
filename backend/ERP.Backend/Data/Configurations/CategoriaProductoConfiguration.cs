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
    /// Configuración de Fluent API para la entidad <see cref="CategoriaProducto"/>.
    /// Mapea la entidad a la tabla <c>CategoriasProducto</c> y define restricciones de columnas e índices.
    /// </summary>
    /// <remarks>
    /// Índices definidos:
    /// <list type="bullet">
    ///   <item><c>UQ_CategoriasProducto_NombreCategoria</c> — único sobre NombreCategoria.</item>
    /// </list>
    /// </remarks>
    public class CategoriaProductoConfiguration : IEntityTypeConfiguration<CategoriaProducto>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<CategoriaProducto> builder)
        {
            builder.ToTable("CategoriasProducto");

            builder.HasKey(c => c.CategoriaID);


            builder.Property(c => c.CategoriaID)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.NombreCategoria)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Descripcion)
                .HasMaxLength(500);


            builder.HasIndex(c => c.NombreCategoria)
                .IsUnique()
                .HasDatabaseName("UQ_CategoriasProducto_NombreCategoria");
        }
    }
}