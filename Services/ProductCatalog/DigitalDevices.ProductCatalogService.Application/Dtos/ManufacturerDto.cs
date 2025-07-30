using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public record ManufacturerDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; init; }
    }
}
