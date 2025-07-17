
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Data
{
    public class ProductCatalogContextFactory : IDesignTimeDbContextFactory<ProductCatalogContext>
    {
        public ProductCatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductCatalogContext>();

            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new ProductCatalogContext(optionsBuilder.Options);
        }
    }
}
