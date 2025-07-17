using System.Reflection;
using System.Text.Json;
using DigitalDevices.ProductTypesService.Core.Models;
using DigitalDevices.ProductTypesService.Infrastructure.Data;

namespace DigitalDevices.ProductTypesService.Infrastructure.DataSeeding
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ProductTypesContext context)
        {

            if (context.ProductTypes.Any())
            {
                return;
            }

            var productTypesJson = await GetJson("ProductTypes.json");

            //var characteristicsSetsJson = await GetJson("TypesList.json");

            var productTypes = JsonSerializer.Deserialize<ProductTypes[]>(productTypesJson);

/*            var characteristicsSets = JsonSerializer.Deserialize<List<string>[]>(characteristicsSetsJson); <-- Добавление существующих типов характеристик к типам продуктов (организуем через брокеры сообщений по id потом)

            for (int i = 0; i < productTypes.Length; i++)
            {
                productTypes[i].
            }*/

            var productNames = productTypes.Select(item => item.Name).ToList();

            await context.ProductTypes.AddRangeAsync(productTypes);

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
