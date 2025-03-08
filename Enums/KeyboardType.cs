using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Enums
{
    public enum KeyboardType
    {
        [Display(Name = "Механическая")]
        Mechanical,
        [Display(Name = "Мембранная")]
        Membrane,
        [Display(Name = "Оптическая")]
        Optical
    }
}
