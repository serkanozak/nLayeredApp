using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(b => b.Id); //primary key set.

            builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
            builder.Property(b => b.CategoryId).HasColumnName("CategoryId");
            builder.Property(b => b.ProductName).HasColumnName("ProductName").IsRequired();
            builder.Property(b => b.UnitPrice).HasColumnName("UnitPrice").IsRequired();
            builder.Property(b => b.UnitsInStock).HasColumnName("UnitsInStock").IsRequired();
            builder.Property(b => b.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsRequired();
            builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");


            builder.HasIndex(indexExpression: b => b.ProductName, name: "UK_Products_ProductName").IsUnique();

            builder.HasOne(b => b.Category);

            builder.HasQueryFilter(b => !b.DeletedDate.HasValue); //default filtre koymak için bunu kullanırız.
        }
    }
}
