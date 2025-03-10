using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;
namespace DigitalDevices.Models
{
    public class Keyboard : Peripheral
    {
        [Display(Name = "Тип")]
        public KeyboardType Type { get; set; }
        [Display(Name = "Свичи")]
        public string Switches { get; set; }
        [Display(Name = "Кейкапы")]
        public KeycapsType Keycaps { get; set; }
        [Display(Name = "Жизненный цикл")]
        public int LifeCycle { get; set; }
        [Display(Name = "Сила нажатия")]
        public int PushStrength { get; set; }
        [Display(Name = "Количество клавиш")]
        public int KeysCount { get; set; }
        [Display(Name = "Материал")]
        public string Material { get; set; }
        public Keyboard()
        {
            
        }
    }
}
