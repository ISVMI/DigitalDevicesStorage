using DigitalDevices.ManufacturersService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ManufacturersService.Infrastructure.Data
{
    public class ManufacturersContext : DbContext
    {

        public ManufacturersContext(DbContextOptions<ManufacturersContext> options) : base(options) { }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                connectionString = connectionString?
                    .Replace("${POSTGRES_HOST}", Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "postgres")
                    .Replace("${POSTGRES_DB}", Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "manufacturers")
                    .Replace("${POSTGRES_USER}", Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "root")
                    .Replace("${POSTGRES_PASSWORD}", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "pa55w0rd!");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }*/

        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
