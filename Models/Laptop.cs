using DigitalDevices.Enums;
using DigitalDevices.Interfaces;
using System.ComponentModel.DataAnnotations;
namespace DigitalDevices.Models
{
    public class Laptop : Product ,IComputing
    {
        [Display(Name = "Операционная система")]
        public OperatingSystems OS { get; set; }
        [Display(Name = "Модель процессора")]
        public string CPModel { get; set; }
        [Display(Name = "Частота процессора")]
        public float CPFrequency { get; set; }
        [Display(Name = "Количество ядер")]
        public int CPcores { get; set; }
        [Display(Name = "Тип оперативной памяти")]
        public RAMTypes RAMType { get; set; }
        [Display(Name = "Размер оперативной памяти")]
        public string RAMGB { get; set; }
        [Display(Name = "Видеокарта")]
        public string GPU { get; set; }
        [Display(Name = "Размер видеопамяти")]
        public string GPUGB { get; set; }
        [Display(Name = "Тип хранилища")]
        public StorageType StorageDriveType { get; set; }
        [Display(Name = "Размер хранилища")]
        public string StorageGB { get; set; }
        public Laptop()
        {
            
        }
    }
}
