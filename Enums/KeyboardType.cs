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
        Optical,
        [Display(Name = "Аналоговая оптическая")]
        OpticAnalog,
        [Display(Name = "Оптомеханическая")]
        OpticMechanic,
        [Display(Name = "Ножничная")]
        Scissors,
        [Display(Name = "Плунжерная")]
        Plunger
    }
}
