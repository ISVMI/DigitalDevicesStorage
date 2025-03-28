using DigitalDevices.Models;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Bogus;
namespace DigitalDevices
{
    public class DbInitializer
    {
        public static void Initialize(DigitalDevicesContext context)
        {
            if (!context.Manufacturers.Any())
            {
                using (FileStream fs = new FileStream("Manufacturers.json", FileMode.OpenOrCreate))
                {
                    Manufacturer[] manufacturers = JsonSerializer.Deserialize<Manufacturer[]>(fs);
                    context.Manufacturers.AddRange(manufacturers);
                }

                context.SaveChanges();
            }
            if (!context.CharacteristicsType.Any())
            {
                using (FileStream fs = new FileStream("CharacteristicTypes.json", FileMode.OpenOrCreate))
                {
                    CharacteristicsType[] characteristicsTypes = JsonSerializer.Deserialize<CharacteristicsType[]>(fs);
                    context.CharacteristicsType.AddRange(characteristicsTypes);
                }

                context.SaveChanges();
            }
            if (!context.ProductTypes.Any())
            {
                List<string> productNames = new();
                using (FileStream fs = new FileStream("ProductTypes.json", FileMode.OpenOrCreate))
                {
                    ProductTypes[] characteristicsSets = JsonSerializer.Deserialize<ProductTypes[]>(fs);
                    foreach (var item in characteristicsSets)
                    {
                        productNames.Add(item.Name);
                    }
                    context.ProductTypes.AddRange(characteristicsSets);
                }
                context.SaveChanges();
                List<string>[] typesList = null;
                using (FileStream fs = new FileStream("TypesList.json", FileMode.OpenOrCreate))
                {
                    typesList = JsonSerializer.Deserialize<List<string>[]>(fs);
                }
                int ind = 0;
                foreach (var productItem in productNames)
                {
                    foreach (var characteristicItem in typesList[ind])
                    {
                        var singleType = context.CharacteristicsType.FirstOrDefault(t => t.Name == characteristicItem);
                        var singleProduct = context.ProductTypes.FirstOrDefault(t => t.Name == productItem);
                        if (singleType != null && singleProduct!=null)
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

            context.SaveChanges();
        }
    }
}
