using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Core.Enums
{
    public enum OperatingPrinciple
    {
        [Display(Name = "Динамический")]
        Dynamic,
        [Display(Name = "Конденсаторный")]
        Condencer,
        [Display(Name = "Электретный")]
        Electret
    }
}
