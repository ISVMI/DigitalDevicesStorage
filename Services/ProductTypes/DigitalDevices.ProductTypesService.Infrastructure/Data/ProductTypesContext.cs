using DigitalDevices.ProductTypesService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ProductTypesService.Infrastructure.Data
{
    public class ProductTypesContext : DbContext
    {
        public ProductTypesContext(DbContextOptions<ProductTypesContext> options) : base(options) { }

        public DbSet<ProductTypes> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTypes>()
                .Property(pt => pt.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<ProductTypes>()
                .Property(pt => pt.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<ProductTypes>()
                .Property(pt => pt.ProductsIds)
                .HasColumnName("ProductsIds");

            modelBuilder.Entity<ProductTypes>()
                .Property(pt => pt.CharacteristicsTypesIds) 
                .HasColumnName("CharacteristicsTypesIds");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
