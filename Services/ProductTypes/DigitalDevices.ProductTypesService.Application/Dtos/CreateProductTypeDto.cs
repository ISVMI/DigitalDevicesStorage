using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Application.Dtos
{
    public record CreateProductTypeDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; init; }
        [Display(Name = "Типы характеристик")]
        public List<Guid> CharacteristicsTypesIds { get; init; } = new();
    }
}
