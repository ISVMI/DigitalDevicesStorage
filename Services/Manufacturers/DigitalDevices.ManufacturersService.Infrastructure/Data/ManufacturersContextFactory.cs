using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitalDevices.ManufacturersService.Infrastructure.Data
{
    public class ManufacturersContextFactory : IDesignTimeDbContextFactory<ManufacturersContext>
    {
        public ManufacturersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ManufacturersContext>();

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new ManufacturersContext(optionsBuilder.Options);
        }
    }
}
