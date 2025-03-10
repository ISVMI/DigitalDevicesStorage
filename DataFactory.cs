using Bogus;
using DigitalDevices.Models;
using System.Linq;
using DigitalDevices.Enums;
namespace DigitalDevices
{
    public class DataFactory
    {
        private readonly DigitalDevicesContext _context;
        private readonly Faker<Computer> _computerFaker;
        private readonly Faker<GraphicalTablet> _graphicalTabletsFaker;
        private readonly Faker<Headphones> _headphonesFaker;
        private readonly Faker<Keyboard> _keyboardsFaker;
        private readonly Faker<Laptop> _laptopsFaker;
        private readonly Faker<Mouse> _miceFaker;
        private readonly Faker<Microphone> _microphonesFaker;
        private readonly Faker<DigitalDevices.Models.Monitor> _monitorsFaker;
        private readonly Faker<Tablet> _tabletsFaker;
        private readonly Faker<TV> _tvsFaker;
        private readonly Faker<WebCam> _webCamsFaker;
        public DataFactory(DigitalDevicesContext context)
        {
            _context = context;

            _computerFaker = new Faker<Computer>()
                .RuleFor(c => c.OS, f => f.PickRandom<OperatingSystems>())
                .RuleFor(c => c.CPModel, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()))
                .RuleFor(c => c.CPFrequency, f => MathF.Round(f.Random.Float(2, 5), 1))
                .RuleFor(c => c.CPcores, f => f.Random.Int(2, 64))
                .RuleFor(c => c.RAMType, f => f.PickRandom<RAMTypes>())
                .RuleFor(c => c.RAMGB, f => $"{f.Random.Int(4, 128)} GB")
                .RuleFor(c => c.GPU, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()))
                .RuleFor(c => c.GPUGB, f => $"{f.Random.Int(2, 48)} GB")
                .RuleFor(c => c.StorageDriveType, f => f.PickRandom<StorageType>())
                .RuleFor(c => c.StorageGB, f => $"{f.Random.Int(128, 22528)} GB")
                .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
                .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
                .RuleFor(c => c.Color, f => f.Commerce.Color())
                .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
                .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _graphicalTabletsFaker = new Faker<GraphicalTablet>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.Diagonal, f => f.Random.Int(11, 24))
    .RuleFor(c => c.Definition, f => f.Random.Replace("####x####"))
    .RuleFor(c => c.FPS, f => f.Random.Int(59, 60))
    .RuleFor(c => c.MatrixType, f => f.PickRandom<MatrixTypes>())
    .RuleFor(c => c.ResponceTime, f => f.Random.Int(200, 300))
    .RuleFor(c => c.WorkWidth, f => MathF.Round(f.Random.Float(100, 500), 1))
    .RuleFor(c => c.WorkHeight, f => MathF.Round(f.Random.Float(100, 300), 1))
    .RuleFor(c => c.TabletDefinition, f => f.Random.Int(5000, 5100))
    .RuleFor(c => c.Sensivity, f => f.Random.Int(8000, 8200))
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1_000_000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _headphonesFaker = new Faker<Headphones>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.HeadphonesType, f => f.PickRandom<HeadphonesType>())
    .RuleFor(c => c.MaxFrequency, f => f.Random.Int(20000, 48000))
    .RuleFor(c => c.Sensivity, f => f.Random.Int(80, 130))
    .RuleFor(c => c.SoundSchemeFormat, f => MathF.Round(f.Random.Float(1, 7.1f), 1))
    .RuleFor(c => c.Microphone, f => f.Random.Bool())
    .RuleFor(c => c.Connection, f => f.PickRandom<AudioConnectionType>())
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _keyboardsFaker = new Faker<Keyboard>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.Type, f => f.PickRandom<KeyboardType>())
    .RuleFor(c => c.Switches, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()) + " " + f.PickRandom("RED", "Orange", "Green", "Blue", "Black"))
    .RuleFor(c => c.Keycaps, f => f.PickRandom<KeycapsType>())
    .RuleFor(c => c.LifeCycle, f => f.Random.Int(10_000_000, 100_000_000))
    .RuleFor(c => c.PushStrength, f => f.Random.Int(10, 100))
    .RuleFor(c => c.KeysCount, f => f.Random.Int(32, 120))
    .RuleFor(c => c.Material, f => f.PickRandom("Металл", "Пластик"))
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _laptopsFaker = new Faker<Laptop>()
    .RuleFor(c => c.OS, f => f.PickRandom<OperatingSystems>())
    .RuleFor(c => c.CPModel, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()))
    .RuleFor(c => c.CPFrequency, f => MathF.Round(f.Random.Float(2, 5), 1))
    .RuleFor(c => c.CPcores, f => f.Random.Int(2, 64))
    .RuleFor(c => c.RAMType, f => f.PickRandom<RAMTypes>())
    .RuleFor(c => c.RAMGB, f => $"{f.Random.Int(4, 128)} GB")
    .RuleFor(c => c.GPU, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()))
    .RuleFor(c => c.GPUGB, f => $"{f.Random.Int(2, 48)} GB")
    .RuleFor(c => c.StorageDriveType, f => f.PickRandom<StorageType>())
    .RuleFor(c => c.StorageGB, f => $"{f.Random.Int(128, 22528)} GB")
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _microphonesFaker = new Faker<Microphone>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.Principle, f => f.PickRandom<OperatingPrinciple>())
    .RuleFor(c => c.Direction, f => f.PickRandom<MicrophoneDirections>())
    .RuleFor(c => c.ExecutionType, f => f.PickRandom<MicrophoneExecutionTypes>())
    .RuleFor(c => c.AudioConnectionType, f => f.PickRandom<AudioConnectionType>())
    .RuleFor(c => c.MinFrequency, f => f.Random.Int(10, 38))
    .RuleFor(c => c.MaxFrequency, f => f.Random.Int(10000, 16000))
    .RuleFor(c => c.Impedance, f => f.Random.Int(100, 2000))
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _monitorsFaker = new Faker<Models.Monitor>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.Diagonal, f => f.Random.Int(11, 24))
    .RuleFor(c => c.Definition, f => f.Random.Replace("####x####"))
    .RuleFor(c => c.FPS, f => f.Random.Int(59, 60))
    .RuleFor(c => c.MatrixType, f => f.PickRandom<MatrixTypes>())
    .RuleFor(c => c.VideoConnector, f => f.PickRandom<VideoConnector>())
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1_000_000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _miceFaker = new Faker<Mouse>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.KeysCount, f => f.Random.Int(0, 25))
    .RuleFor(c => c.DPI, f => f.Random.Int(200, 42000))
    .RuleFor(c => c.Frequency, f => f.Random.Int(25, 2000))
    .RuleFor(c => c.MaxAcceleration, f => f.Random.Int(8, 22))
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1000000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _tabletsFaker = new Faker<Tablet>()
    .RuleFor(c => c.Diagonal, f => f.Random.Int(11, 24))
    .RuleFor(c => c.Definition, f => f.Random.Replace("####x####"))
    .RuleFor(c => c.FPS, f => f.Random.Int(59, 60))
    .RuleFor(c => c.MatrixType, f => f.PickRandom<MatrixTypes>())
    .RuleFor(c => c.CPU, f => f.PickRandom(_context.Manufacturers.Select(x => x.Name).ToList()))
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1_000_000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _tvsFaker = new Faker<TV>()
    .RuleFor(c => c.Diagonal, f => f.Random.Int(11, 24))
    .RuleFor(c => c.Definition, f => f.Random.Replace("####x####"))
    .RuleFor(c => c.FPS, f => f.Random.Int(59, 60))
    .RuleFor(c => c.MatrixType, f => f.PickRandom<MatrixTypes>())
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1_000_000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));

            _webCamsFaker = new Faker<WebCam>()
    .RuleFor(c => c.ConnectionType, f => f.PickRandom<ConnectionType>())
    .RuleFor(c => c.MegaPixels, f => f.Random.Int(1, 48))
    .RuleFor(c => c.Definition, f => f.Random.Replace("####x####"))
    .RuleFor(c => c.FPS, f => f.Random.Int(59, 60))
    .RuleFor(c => c.Microphone, f => f.Random.Bool())
    .RuleFor(c => c.Price, f => MathF.Round(f.Random.Float(15000, 1_000_000), 2))
    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
    .RuleFor(c => c.Model, f => f.Random.Int(2, 10000).ToString())
    .RuleFor(c => c.Color, f => f.Commerce.Color())
    .RuleFor(c => c.Warranty, f => f.Random.Int(1, 10))
    .RuleFor(c => c.ManufacturerId, f => f.PickRandom(_context.Manufacturers.Select(x => x.Id).ToList()));
        }

        public Computer GenerateComputer() => _computerFaker.Generate();
        public GraphicalTablet GenerateGraphicalTablet() => _graphicalTabletsFaker.Generate();
        public Headphones GenerateHeadphones() => _headphonesFaker.Generate();
        public Keyboard GenerateKeyboard() => _keyboardsFaker.Generate();
        public Laptop GenerateLaptop() => _laptopsFaker.Generate();
        public Microphone GenerateMicrophone() => _microphonesFaker.Generate();
        public Models.Monitor GenerateMonitor() => _monitorsFaker.Generate();
        public Mouse GenerateMouse() => _miceFaker.Generate();
        public Tablet GenerateTablet() => _tabletsFaker.Generate();
        public TV GenerateTV() => _tvsFaker.Generate();
        public WebCam GenerateWebCam() => _webCamsFaker.Generate();
    }
}
