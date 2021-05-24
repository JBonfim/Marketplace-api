using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Domain.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Image)
               .HasColumnType("varchar(1000)");



            builder.ToTable("product");
        }
    }
}
