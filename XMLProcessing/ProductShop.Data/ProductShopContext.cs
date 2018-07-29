namespace ProductShop.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProductShop.Data.Configurations;
    using ProductShop.Models;
    using System;

    public class ProductShopContext : DbContext
    {
        public ProductShopContext()
        {
        }
        public ProductShopContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProducts> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CategoryProductsConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }
    }
}
