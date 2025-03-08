using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Enums
{
    public enum HeadphonesType
    {
        [Display(Name = "Вкладыши")]
        Earbuds,
        [Display(Name = "Внутриканальные")]
        InEar,
        [Display(Name = "Накладные")]
        OverEar,
        [Display(Name = "Охватывающие")]
        Covering
    }
}
