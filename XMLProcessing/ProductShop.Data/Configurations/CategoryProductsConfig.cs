namespace ProductShop.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductShop.Models;

    public class CategoryProductsConfig : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(x => new { x.CategoryId, x.ProductId });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.CategoryProducts)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.CategoryProducts)
                .HasForeignKey(x => x.ProductId);

        }
    }
}
