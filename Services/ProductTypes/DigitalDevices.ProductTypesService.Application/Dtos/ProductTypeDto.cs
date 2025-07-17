using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Application.Dtos
{
    public record ProductTypeDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; init; }
    }
}
