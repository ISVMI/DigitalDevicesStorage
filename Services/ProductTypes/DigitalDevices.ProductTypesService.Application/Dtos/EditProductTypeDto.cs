using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Application.Dtos
{
    public record EditProductTypeDto
    {
        public int Id { get; init; }
        [Display(Name = "Наименование")]
        public string Name { get; init; }
        [Display(Name = "Добавить характеристики:")]
        public List<string> CharacteristicTypesToAdd { get; init; } = new();
        [Display(Name = "Удалить характеристики:")]
        public List<string> CharacteristicTypesToDelete { get; init; } = new();
    }
}
