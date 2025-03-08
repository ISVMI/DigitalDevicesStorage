using DigitalDevices.Enums;

namespace DigitalDevices.Interfaces
{
    public interface IDisplayable
    {
        public int Diagonal { get; set; }
        public string Definition { get; set; }
        public float FPS { get; set; }
        public MatrixTypes MatrixType { get; set; }
    }
}
