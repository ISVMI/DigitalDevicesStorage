namespace DigitalDevices.Models
{
    public class DataViewModel
    {
        public List<Computer> ComputersData { get; set; } = new List<Computer>();
        public List<GraphicalTablet> GraphicalTabletsData { get; set; } = new List<GraphicalTablet>();
        public List<Headphones> HeadphonesData { get; set; } = new List<Headphones>();
        public List<Keyboard> KeyboardsData { get; set; } = new List<Keyboard>();
        public List<Laptop> LaptopsData { get; set; } = new List<Laptop>();
        public List<Microphone> MicrophonesData { get; set; } = new List<Microphone>();
        public List<Monitor> MonitorsData { get; set; } = new List<Monitor>();
        public List<Mouse> MiceData { get; set; } = new List<Mouse>();
        public List<Tablet> TabletsData { get; set; } = new List<Tablet>();
        public List<TV> TVsData { get; set; } = new List<TV>();
        public List<WebCam> WebCamsData { get; set; } = new List<WebCam>();
        public List<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
    }
}
