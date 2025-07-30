using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Enums
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
