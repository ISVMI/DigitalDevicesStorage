using Bogus;
using DigitalDevices.DataContext;
using DigitalDevices.Enums;
using DigitalDevices.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalDevices.DataSeeding
{
    public class ProductGenerator
    {
        private readonly DigitalDevicesContext _context;
        private readonly Faker _faker = new ("ru");

        public ProductGenerator(DigitalDevicesContext context)
        {
            _context = context;
        }

        public void GenerateProducts(int count)
        {
            var manufacturers = _context.Manufacturers.ToList();
            var productTypes = _context.ProductTypes
                .Include(pt => pt.CharacteristicsTypeProductTypes)
                .ThenInclude(ctpt => ctpt.CharacteristicsTypes)
                .ToList();

            var products = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                var productType = _faker.PickRandom(productTypes);
                var manufacturer = _faker.PickRandom(manufacturers);

                var product = new Product
                {
                    Name = GenerateProductName(productType.Name),
                    Model = _faker.Random.AlphaNumeric(8).ToUpper(),
                    Price = (decimal)_faker.Finance.Amount(100, 1000000),
                    Color = _faker.Commerce.Color(),
                    Warranty = _faker.Random.Int(12, 60),
                    Manufacturer = manufacturer,
                    ProductTypes = productType
                };

                foreach (var ct in productType.CharacteristicsTypeProductTypes)
                {
                    var characteristic = new Characteristics
                    {
                        CharacteristicsType = ct.CharacteristicsTypes,
                        Value = GenerateCharacteristicValue(ct.CharacteristicsTypes)
                    };
                    _context.Characteristics.Add(characteristic);

                    var characteristicProduct = new CharacteristicsProduct
                    {
                        Characteristics = characteristic,
                        Products = product
                    };
                    product.CharacteristicsProduct.Add(characteristicProduct);
                    _context.CharacteristicsProducts.Add(characteristicProduct);

                }

                _context.Products.Add(product);
            }
            _context.SaveChanges();
        }

        private string GenerateProductName(string productType)
        {
            return productType switch
            {
                "Компьютер" => _faker.Commerce.ProductName() + " PC",
                "Ноутбук" => $"{_faker.Company.CompanyName()} {_faker.Random.Word().ToUpper()}",
                "Наушники" => $"{_faker.Commerce.ProductAdjective()} Headphones {_faker.Random.AlphaNumeric(3)}",
                _ => _faker.Commerce.ProductName()
            };
        }

        private string GenerateCharacteristicValue(CharacteristicsType characteristicType)
        {
            return characteristicType.DataType switch
            {
                "int" when characteristicType.Name == "Количество клавиш" =>
                _faker.Random.Int(14, 124).ToString(),
                "int" when characteristicType.Name == "Размер оперативной памяти (ГБ)" => 
                _faker.PickRandom(4,8,12,16,24,32,64,96).ToString(),
                "int" when characteristicType.Name == "Объём видеопамяти (ГБ)" =>
                _faker.PickRandom(2,4,6,8,10,12,16,24).ToString(),
                "int" when characteristicType.Name == "Объём накопителя (ГБ)" =>
                _faker.PickRandom(64,128,240,250,256,500,512,1000,1024,1500,2000,2048,2500,4500).ToString(),
                "float" when characteristicType.Name == "Диагональ (в дюймах)" => 
                _faker.Random.Float(10, 32).ToString("0.0"),
                "float" when characteristicType.Name == "Рабочая ширина (мм)" =>
                _faker.Random.Float(101, 526.9f).ToString("0.0"),
                "float" when characteristicType.Name == "Рабочая высота (мм)" =>
                _faker.Random.Float(66, 296.4f).ToString("0.0"),
                "int" when characteristicType.Name == "Минимальная частота (Гц)" => 
                _faker.Random.Int(18, 150).ToString(),
                "float" when characteristicType.Name == "Количество мегапикселей" => 
                _faker.Random.Float(0.3f, 48).ToString("0.0"),
                "int" when characteristicType.Name == "Максимальная частота (Гц)" => 
                _faker.Random.Int(10000, 40000).ToString(),
                "int" when characteristicType.Name == "Количество ядер" => 
                _faker.Random.Int(2, 16).ToString(),
                "int" => _faker.Random.Int(2, 128).ToString(),
                "float" => _faker.Random.Float(0.1f, 100).ToString("0.0"),
                "enum" when characteristicType.Name == "Операционная система" =>
                    _faker.PickRandom<OperatingSystems>().ToString(),
                "enum" when characteristicType.Name == "Разъём подключения" =>
                _faker.PickRandom<AudioConnectionType>().ToString(),
                "enum" when characteristicType.Name == "Тип подключения" =>
                    _faker.PickRandom<ConnectionType>().ToString(),
                "enum" when characteristicType.Name == "Тип клавиатуры" =>
                _faker.PickRandom<KeyboardType>().ToString(),
                "enum" when characteristicType.Name == "Кейкапы" =>
                    _faker.PickRandom<KeycapsType>().ToString(),
                "enum" when characteristicType.Name == "Тип матрицы" =>
                _faker.PickRandom<MatrixTypes>().ToString(),
                "enum" when characteristicType.Name == "Направленность" =>
                    _faker.PickRandom<MicrophoneDirections>().ToString(),
                "enum" when characteristicType.Name == "Вид исполнения" =>
                _faker.PickRandom<MicrophoneExecutionTypes>().ToString(),
                "enum" when characteristicType.Name == "Принцип действия" =>
                    _faker.PickRandom<OperatingPrinciple>().ToString(),
                "enum" when characteristicType.Name == "Тип оперативной памяти" =>
                _faker.PickRandom<RAMTypes>().ToString(),
                "enum" when characteristicType.Name == "Тип накопителя" =>
                    _faker.PickRandom<StorageType>().ToString(),
                "enum" when characteristicType.Name == "Разъём мультимедиа интерфейса" =>
                _faker.PickRandom<VideoConnector>().ToString(),
                "enum" when characteristicType.Name == "Тип наушников" =>
                _faker.PickRandom<HeadphonesType>().ToString(),

                _ => characteristicType.Name switch
                {
                    "Свичи" => _faker.Company.CompanyName() + " " + _faker.Commerce.Color(),
                    "Разрешение изображения" => $"{_faker.Random.Int(1280, 3840)}x{_faker.Random.Int(720, 2160)}",
                    "Наличие микрофона" => _faker.Random.Bool().ToString(),
                    "Цвет" => _faker.Commerce.Color(),
                    "Модель процессора" => $"Core {_faker.PickRandom("i3", "i5", "i7", "i9")}-{_faker.Random.Int(1000, 12000)}",
                    _ => _faker.Lorem.Word()
                }
            };
        }
    }
}
