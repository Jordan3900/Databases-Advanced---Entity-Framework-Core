namespace CarDealer.Data
{
    using CarDealer.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
        {
        }
        public CarDealerContext(DbContextOptions options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Configuration.connection);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PartCar>()
                .HasKey(x => new { x.PartId, x.CarId });

            builder.Entity<PartCar>()
                .HasOne(x => x.Car)
                .WithMany(x => x.PartCars)
                .HasForeignKey(x => x.CarId);

            builder.Entity<PartCar>()
                .HasOne(x => x.Part)
                .WithMany(x => x.PartCars)
                .HasForeignKey(x => x.PartId);
        }
    }
}
