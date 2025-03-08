using DigitalDevices.Enums;

namespace DigitalDevices.Interfaces
{
    public interface IComputing
    {
        public OperatingSystems OS { get; set; }
        public string CPModel { get; set; }
        public float CPFrequency { get; set; }
        public int CPcores { get; set; }
        public RAMTypes RAMType { get; set; }
        public string RAMGB { get; set; }
        public string GPU { get; set; }
        public string GPUGB { get; set; }
        public StorageType StorageDriveType { get; set; }
        public string StorageGB { get; set; }

    }
}
