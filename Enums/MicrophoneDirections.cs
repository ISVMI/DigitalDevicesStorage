using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Enums
{
    public enum MicrophoneDirections
    {
        [Display(Name = "Всенаправленный")]
        Omnidirectional,
        [Display(Name = "Кардиоид")]
        Cardioid,
        [Display(Name = "Гиперкардиоид")]
        Hypercardioid,
        [Display(Name = "Суперакардиоид")]
        Supercardioid,
        [Display(Name = "Двунаправленный")]
        BiDirectional
    }
}
