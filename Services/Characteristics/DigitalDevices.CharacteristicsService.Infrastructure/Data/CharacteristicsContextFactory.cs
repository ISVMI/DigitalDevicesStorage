using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Data
{
    public class CharacteristicsContextFactory : IDesignTimeDbContextFactory<CharacteristicsContext>
    {
        public CharacteristicsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CharacteristicsContext>();
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            return new CharacteristicsContext(optionsBuilder.Options);
        }
    }
}
