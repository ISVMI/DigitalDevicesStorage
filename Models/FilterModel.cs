namespace DigitalDevices.Models
{
    public class FilterModel
    {
        public PriceFilter Price { get; set; }
        public List<string> Manufacturers { get; set; }
        public List<string> RAM { get; set; }
        public List<string> DriveVolume { get; set; }
        public List<string> OperatingSystem { get; set; }
        public List<string> CoresQuantity { get; set; }
        public TabletWidthsFilter TabletWidths { get; set; }
        public List<string> HeadphonesType { get; set; }
        public List<string> AudioScheme { get; set; }
        public List<string> KeyboardType { get; set; }
        public KeysCountFilter KeysCount { get; set; }
        public List<string> Switches { get; set; }
        public MouseKeysCountFilter MouseKeysCount { get; set; }
        public List<string> DPI { get; set; }
        public List<string> ExecutionType { get; set; }
        public List<string> Direction { get; set; }
        public List<string> MinFrequency { get; set; }
        public List<string> MaxFrequency { get; set; }
        public MonitorFpsFilter MonitorFps { get; set; }
        public MegapixelsFilter Megapixels { get; set; }
        public List<string> MicrophonePresence { get; set; }
        public List<string> FPS { get; set; }
        public DiagonalFilter Diagonal { get; set; }
        public List<string> MatrixType { get; set; }
        public List<string> AudioConnection { get; set; }
        public List<string> Connection { get; set; }
    }

    public class PriceFilter
    {
        public float? Min { get; set; }
        public float? Max { get; set; }
    }
    public class TabletWidthsFilter
    {
        public float? Min { get; set; }
        public float? Max { get; set; }
    }
    public class KeysCountFilter
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
    public class MouseKeysCountFilter
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
    public class MonitorFpsFilter
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
    public class MegapixelsFilter
    {
        public float? Min { get; set; }
        public float? Max { get; set; }
    }
    public class DiagonalFilter
    {
        public float? Min { get; set; }
        public float? Max { get; set; }
    }
}
