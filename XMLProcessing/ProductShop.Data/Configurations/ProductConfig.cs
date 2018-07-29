namespace ProductShop.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductShop.Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Buyer)
                .WithMany(x => x.BoughtProducts)
                .HasForeignKey(x => x.BuyerId);

            builder.HasOne(x => x.Seller)
                .WithMany(x => x.SoldProducts)
                .HasForeignKey(x => x.SellerId);
        }
    }
}
