using Microsoft.EntityFrameworkCore;
using DigitalDevices.Models;
namespace DigitalDevices
{
    public class DigitalDevicesContext : DbContext
    {
        public DigitalDevicesContext(DbContextOptions<DigitalDevicesContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=DigitalDevices;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<CharacteristicsType> CharacteristicsType { get; set; }
        public DbSet<CharacteristicsProduct> CharacteristicsProducts { get; set; }
        public DbSet<CharacteristicsTypeProductTypes> CharacteristicsTypeProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacteristicsTypeProductTypes>()
    .ToTable("CharacteristicsTypeProductTypes")
    .HasKey(ptct => new { ptct.ProductTypesId, ptct.CharacteristicsTypeId });

            modelBuilder.Entity<CharacteristicsProduct>()
    .ToTable("CharacteristicsProduct")
    .HasKey(cp => new { cp.ProductId, cp.CharacteristicsId });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductTypes)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.ProductTypesId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.CharacteristicsProduct)
                .WithOne(ct => ct.Products)
                .HasForeignKey(ct => ct.ProductId);

            modelBuilder.Entity<Characteristics>()
                .HasMany(c => c.CharacteristicsProduct)
                .WithOne(ct => ct.Characteristics)
                .HasForeignKey(ct => ct.CharacteristicsId);

            modelBuilder.Entity<Characteristics>()
                .HasOne(c => c.CharacteristicsType)
                .WithMany(t => t.Characteristics)
                .HasForeignKey(c => c.CharacteristicsTypeId);

            modelBuilder.Entity<CharacteristicsType>()
                .HasMany(t => t.CharacteristicsTypeProductTypes)
                .WithOne(pc => pc.CharacteristicsTypes)
                .HasForeignKey(pt => pt.CharacteristicsTypeId);

            modelBuilder.Entity<ProductTypes>()
                .HasMany(pt => pt.CharacteristicsTypeProductTypes)
                .WithOne(pc => pc.ProductTypes)
                .HasForeignKey(pc => pc.ProductTypesId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
