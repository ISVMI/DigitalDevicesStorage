using DigitalDevices.CharacteristicsService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Data
{
    public class CharacteristicsContext : DbContext
    {

        public CharacteristicsContext(DbContextOptions<CharacteristicsContext> options) : base(options) { }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<CharacteristicsType> CharacteristicsType { get; set; }
        public DbSet<CharacteristicsTypeProductTypes> CharacteristicsTypeProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CharacteristicsTypeProductTypes>()
                .HasKey(ptct => new { ptct.ProductTypesId, ptct.CharacteristicsTypeId });

            modelBuilder.Entity<Characteristics>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Characteristics>()
                .HasOne(c => c.CharacteristicsType)
                .WithMany(ct => ct.Characteristics)
                .HasForeignKey(c => c.CharacteristicsTypeId);

            modelBuilder.Entity<Characteristics>()
                .Property(c => c.Value)
                .HasColumnName("Value");

            modelBuilder.Entity<CharacteristicsType>()
                .HasKey(ct => ct.Id);

            modelBuilder.Entity<CharacteristicsType>()
                .Property(ct => ct.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<CharacteristicsType>()
                .Property(ct => ct.DataType)
                .HasColumnName("DataType");

            modelBuilder.Entity<CharacteristicsType>()
                .Property(ct => ct.EnumType)
                .HasColumnName("EnumType");

            base.OnModelCreating(modelBuilder);

        }
    }
}
