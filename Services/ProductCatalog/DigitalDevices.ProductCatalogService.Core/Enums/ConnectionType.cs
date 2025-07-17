using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Core.Enums
{
    public enum ConnectionType
    {
        [Display(Name = "Беспроводное подключение")]
        Wireless,
        [Display(Name = "Проводное подключение")]
        Wired
    }
}
