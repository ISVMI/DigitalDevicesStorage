using DigitalDevices.ManufacturersService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ManufacturersService.Infrastructure.Data
{
    public class ManufacturersContext : DbContext
    {

        public ManufacturersContext(DbContextOptions<ManufacturersContext> options) : base(options) { }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Manufacturer>()
            .Property(m => m.Name)
            .HasColumnName("Name");

            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.Country)
                .HasColumnName("Country");

            modelBuilder.Entity<Manufacturer>()
                .Property(m => m.Address)
                .HasColumnName("Address");

            base.OnModelCreating(modelBuilder);
        }

    }
}
