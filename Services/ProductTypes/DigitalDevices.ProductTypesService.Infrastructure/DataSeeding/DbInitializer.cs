using DigitalDevices.ProductTypesService.Core.Models;
using DigitalDevices.ProductTypesService.Infrastructure.Data;
using MassTransit;
using Shared.Messages;
using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDevices.ProductTypesService.Infrastructure.DataSeeding
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services, ProductTypesContext context)
        {

            if (context.ProductTypes.Any())
            {
                return;
            }

            var publisher = services.GetRequiredService<IPublishEndpoint>();

            var productTypesJson = await GetJson("ProductTypes.json");

            var characteristicsSetsJson = await GetJson("TypesList.json");

            var productTypes = JsonSerializer.Deserialize<ProductTypes[]>(productTypesJson);

            var characteristicsSets = JsonSerializer.Deserialize<List<Guid>[]>(characteristicsSetsJson);

            await context.ProductTypes.AddRangeAsync(productTypes);

            for (int i = 0; i < productTypes.Length; i++)
            {
                var newProductTypeAddedMessage = new ProductTypeCreated(productTypes[i].Id, characteristicsSets[i]);
                await publisher.Publish(newProductTypeAddedMessage);
            }

            await context.SaveChangesAsync();
        }

        private static string GetResourceName(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.FirstOrDefault(r => r.EndsWith(fileName));

            if (resourceName == null)
                throw new FileNotFoundException($"Resource '{fileName}' not found.");

            return resourceName;
        }

        private static async Task<string> GetJson(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var resource = GetResourceName(resourceName);

            await using var stream = assembly.GetManifestResourceStream(resource);

            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            return json;
        }
    }
}
