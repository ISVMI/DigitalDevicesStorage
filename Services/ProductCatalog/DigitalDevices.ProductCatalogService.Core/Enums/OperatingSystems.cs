using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Core.Enums
{
    public enum OperatingSystems
    {
        WindowsXP,
        Windows7,
        WindowsVista,
        Windows8,
        Windows10,
        Windows11,
        Linux,
        MacOs,
        [Display(Name = "Без ос")]
        NoOs
    }
}
