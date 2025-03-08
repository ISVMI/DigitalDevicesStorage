using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Headphones : Peripheral
    {
        [Display(Name = "Тип")]
        public HeadphonesType HeadphonesType { get; set; }
        [Display(Name = "Максимальная частота")]
        public int MaxFrequency { get; set; }
        [Display(Name = "Чувствительность")]
        public int Sensivity { get; set; }
        [Display(Name = "Формат звуковой схемы")]
        public float SoundSchemeFormat { get; set; }
        [Display(Name = "Микрофон")]
        public bool Microphone { get; set; }
        [Display(Name = "Тип подключения")]
        public AudioConnectionType Connection { get; set; }
        public Headphones()
        {
            
        }
    }
}
