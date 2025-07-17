using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Enums
{
    public enum MicrophoneExecutionTypes
    {
        [Display(Name = "Накамерный")]
        OnCamera,
        [Display(Name = "Настольный")]
        Desktop,
        [Display(Name = "Петличный")]
        Lavalier,
        [Display(Name = "Подвесной")]
        Overhead,
        [Display(Name = "Ручной")]
        Handheld
    }
}
