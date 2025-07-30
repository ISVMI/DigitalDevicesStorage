using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Enums
{
    public enum ConnectionType
    {
        [Display(Name = "Беспроводное подключение")]
        Wireless,
        [Display(Name = "Проводное подключение")]
        Wired
    }
}
