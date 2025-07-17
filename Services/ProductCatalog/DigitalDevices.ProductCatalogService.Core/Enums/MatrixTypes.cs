using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Core.Enums
{
    public enum MatrixTypes
    {
        LED,
        IPS,
        VA,
        TFT,
        [Display(Name = "Нет(графический планшет)")]
        No
    }
}
