using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public record CharacteristicsTypesDto
    {
        [Display(Name = "Наименование")]
        public string CharacteristicTypeName { get; init; }
        [Display(Name = "Значение")]
        public string Value { get; init; }
    }
}
