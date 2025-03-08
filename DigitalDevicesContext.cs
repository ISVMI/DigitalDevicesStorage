using Microsoft.EntityFrameworkCore;
using DigitalDevices.Models;
namespace DigitalDevices
{
    public class DigitalDevicesContext : DbContext
    {
        public DigitalDevicesContext(DbContextOptions<DigitalDevicesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=DigitalDevices;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<GraphicalTablet> GraphicalTablets { get; set; }
        public DbSet<Headphones> Headphones { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Mouse> Mouse { get; set; }
        public DbSet<Microphone> Microphones { get; set; }
        public DbSet<Models.Monitor> Monitors { get; set; }
        public DbSet<Tablet> Tablets { get; set; }
        public DbSet<TV> TVs { get; set; }
        public DbSet<WebCam> WebCams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>().ToTable("Computers");
            modelBuilder.Entity<GraphicalTablet>().ToTable("GraphicalTablets");
            modelBuilder.Entity<Headphones>().ToTable("Headphones");
            modelBuilder.Entity<Keyboard>().ToTable("Keyboards");
            modelBuilder.Entity<Laptop>().ToTable("Laptops");
            modelBuilder.Entity<Microphone>().ToTable("Microphones");
            modelBuilder.Entity<Models.Monitor>().ToTable("Monitors");
            modelBuilder.Entity<Tablet>().ToTable("Tablets");
            modelBuilder.Entity<TV>().ToTable("TVs");
            modelBuilder.Entity<WebCam>().ToTable("WebCams");
            modelBuilder.Entity<Mouse>().ToTable("Mice");
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Manufacturer)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ManufacturerId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
