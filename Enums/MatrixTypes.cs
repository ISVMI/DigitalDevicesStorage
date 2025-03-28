using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Enums
{
    public enum MatrixTypes
    {
        LED,
        IPS,
        VA,
        TFT,
        [Display(Name="Нет(графический планшет)")]
        No
    }
}
