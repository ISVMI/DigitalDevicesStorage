using DigitalDevices.Models;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Bogus;
using DigitalDevices.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace DigitalDevices.DataSeeding
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(DigitalDevicesContext context, RolesSeeder seeder)
        {
                if (!context.Manufacturers.Any())
                {
                    using (FileStream fs = new("DataSeeding\\Manufacturers.json", FileMode.Open))
                    {
                        Manufacturer[] manufacturers = JsonSerializer.Deserialize<Manufacturer[]>(fs);
                        await context.Manufacturers.AddRangeAsync(manufacturers);
                    }
                }

            if (!context.CharacteristicsType.Any())
            {
                using (FileStream fs = new ("DataSeeding\\CharacteristicTypes.json", FileMode.Open))
                {
                    CharacteristicsType[] characteristicsTypes = JsonSerializer.Deserialize<CharacteristicsType[]>(fs);
                    await context.CharacteristicsType.AddRangeAsync(characteristicsTypes);
                }

            }

                if (!context.ProductTypes.Any())
                {
                    List<string> productNames = new();
                    using (FileStream fs = new("DataSeeding\\ProductTypes.json", FileMode.Open))
                    {
                        ProductTypes[] characteristicsSets = JsonSerializer.Deserialize<ProductTypes[]>(fs);
                        foreach (var item in characteristicsSets)
                        {
                            productNames.Add(item.Name);
                        }
                        await context.ProductTypes.AddRangeAsync(characteristicsSets);
                    }
                    List<string>[] typesList = null;
                    using (FileStream fs = new("DataSeeding\\TypesList.json", FileMode.Open))
                    {
                        typesList = JsonSerializer.Deserialize<List<string>[]>(fs);
                    }
                    int ind = 0;
                    foreach (var productItem in productNames)
                    {
                        foreach (var characteristicItem in typesList[ind])
                        {
                            var singleType = await context.CharacteristicsType.FirstOrDefaultAsync(t => t.Name == characteristicItem);
                            var singleProduct = await context.ProductTypes.FirstOrDefaultAsync(t => t.Name == productItem);
                            if (singleType != null && singleProduct != null)
                            {
                                singleProduct.CharacteristicsTypeProductTypes.Add(new()
                                {
                                    ProductTypes = singleProduct,
                                    ProductTypesId = singleProduct.Id,
                                    CharacteristicsTypes = singleType,
                                    CharacteristicsTypeId = singleType.Id
                                });
                            }
                        }
                        ind++;
                    }
                }

            if (!context.Products.Any())
            {
                var generator = new ProductGenerator(context);
                generator.GenerateProducts(5500);
            }
            await seeder.SeedRolesAsync();
                await context.SaveChangesAsync();
        }
    }
}
