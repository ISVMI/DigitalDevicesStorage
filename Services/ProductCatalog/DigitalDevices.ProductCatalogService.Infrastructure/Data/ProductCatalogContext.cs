using DigitalDevices.ProductCatalogService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Data
{
    public class ProductCatalogContext : DbContext
    {

        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.Price)
                .HasColumnName("Price")
                .HasColumnType("numeric(18,2)");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.Model)
                .HasColumnName("Model");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.Color)
                .HasColumnName("Color");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.Warranty)
                .HasColumnName("Warranty");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.ManufacturerId)
                .HasColumnName("ManufacturerId");

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .Property(p => p.ProductTypesId)
                .HasColumnName("ProductTypesId");

            base.OnModelCreating(modelBuilder);
        }
    }
}
