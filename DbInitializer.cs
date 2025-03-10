using DigitalDevices.Models;
using Microsoft.IdentityModel.Tokens;
namespace DigitalDevices
{
    public class DbInitializer
    {
        public static void Initialize(DigitalDevicesContext context, int count = 10)
        {
            var factory = new DataFactory(context);
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer() { Name = "ASUS", Country = "Тайвань", Address = "Бэйтоу, Тайбэй" },
                    new Manufacturer() { Name = "Logitech", Country = "Швейцария", Address = "Лозанна" },
                    new Manufacturer() { Name = "ASRock", Country = "Тайвань", Address = "Тайбэй, Тайбэй" },
                    new Manufacturer() { Name = "MSI", Country = "Тайвань", Address = "Бэйтоу, Тайбэй" },
                    new Manufacturer() { Name = "Apple", Country = "США", Address = "Купертино (Калифорния)" },
                    new Manufacturer() { Name = "HYPERPC", Country = "Россия", Address = "Москва" },
                    new Manufacturer() { Name = "Lenovo", Country = "Китай", Address = "Пекин" },
                    new Manufacturer() { Name = "XP-Pen", Country = "Китай", Address = "Шэньчжэнь" },
                    new Manufacturer() { Name = "Wacom", Country = "Япония", Address = "Кадзо, Сайтама" },
                    new Manufacturer() { Name = "Huion", Country = "Китай", Address = "Шэньчжэнь" },
                    new Manufacturer() { Name = "LG", Country = "Южная Корея", Address = "Сеул" },
                    new Manufacturer() { Name = "Xiaomi", Country = "Китай", Address = "Хайдянь, Пекин" },
                    new Manufacturer() { Name = "Samsung", Country = "Южная Корея", Address = "Сеул" },
                    new Manufacturer() { Name = "Razer", Country = "США", Address = "Ирвайн, Калифорния" },
                    new Manufacturer() { Name = "SteelSeries", Country = "Дания", Address = "Фредериксборг" },
                    new Manufacturer() { Name = "Corsair", Country = "США", Address = "Бейсайд-Паркуэй, Фремонт, Калифорния" },
                    new Manufacturer() { Name = "Fifine", Country = "Китай", Address = "-" },
                    new Manufacturer() { Name = "Shure", Country = "США", Address = "Найлс, Иллинойс" },
                    new Manufacturer() { Name = "Sony", Country = "Япония", Address = "Минато, Токио" },
                    new Manufacturer() { Name = "HyperX", Country = "США", Address = "Пало-Альто (Калифорния)" },
                    new Manufacturer() { Name = "Hp", Country = "США", Address = "Пало-Альто (Калифорния)" },
                    new Manufacturer() { Name = "Sennheiser", Country = "Германия", Address = "Ведемарк (Нижняя Саксония)" },
                    new Manufacturer() { Name = "Huawei", Country = "Китай", Address = "Шэньчжэнь" },
                    new Manufacturer() { Name = "Intel", Country = "США", Address = "Санта-Клара, Калифорния" },
                    new Manufacturer() { Name = "AMD", Country = "США", Address = "Санта-Клара, Калифорния" }
                );
            context.SaveChanges();
        }
            for (int i = 0; i < count; i++)
            {
                if (!context.Computers.Any())
                {
                    Computer computer = factory.GenerateComputer();
                    context.Computers.Add(computer);
                }
                if (!context.GraphicalTablets.Any())
                {
                    GraphicalTablet graphicalTablet = factory.GenerateGraphicalTablet();
                    context.GraphicalTablets.Add(graphicalTablet);
                }
                if (!context.Headphones.Any())
                {
                    Headphones headphones = factory.GenerateHeadphones();
                    context.Headphones.Add(headphones);
                }
                if (!context.Keyboards.Any())
                {
                    Keyboard keyboard = factory.GenerateKeyboard();
                    context.Keyboards.Add(keyboard);
                }
                if (!context.Laptops.Any())
                {
                    Laptop laptop = factory.GenerateLaptop();
                    context.Laptops.Add(laptop);
                }
                if (!context.Microphones.Any())
                {
                    Microphone microphone = factory.GenerateMicrophone();
                    context.Microphones.Add(microphone);
                }
                if (!context.Monitors.Any())
                {
                    Models.Monitor monitor = factory.GenerateMonitor();
                    context.Monitors.Add(monitor);
                }
                if (!context.Mouse.Any())
                {
                    Mouse mouse = factory.GenerateMouse();
                    context.Mouse.Add(mouse);
                }
                if (!context.Tablets.Any())
                {
                    Tablet tablet = factory.GenerateTablet();
                    context.Tablets.Add(tablet);
                }
                if (!context.TVs.Any())
                {
                    TV tv = factory.GenerateTV();
                    context.TVs.Add(tv);
                }
                if (!context.WebCams.Any())
                {
                    WebCam webCam = factory.GenerateWebCam();
                    context.WebCams.Add(webCam);
                }
            }
            context.SaveChanges();
        }
    }
}
