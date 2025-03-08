using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Mouse : Peripheral
    {
        [Display(Name = "Количество кнопок")]
        public int KeysCount { get; set; }
        public int DPI { get; set; }
        [Display(Name = "Частота опроса")]
        public int Frequency { get; set; }
        [Display(Name = "Максимальное ускорение")]
        public int MaxAcceleration { get;set; }
        public Mouse()
        {
            
        }
    }
}
