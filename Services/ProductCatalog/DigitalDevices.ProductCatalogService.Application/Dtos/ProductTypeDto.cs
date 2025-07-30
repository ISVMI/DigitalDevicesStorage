using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public record ProductTypeDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}
