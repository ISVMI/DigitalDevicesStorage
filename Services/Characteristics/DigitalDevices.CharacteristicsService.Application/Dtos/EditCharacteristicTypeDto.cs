using System.ComponentModel.DataAnnotations;
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record EditCharacteristicTypeDto
    {
        public Guid Id { get; init; }
        [Display(Name = "Наименование")]
        public string Name { get; init; }
        [Display(Name = "Тип данных")]
        public string DataType { get; init; }
        [Display(Name = "Тип перечисления")]
        public string EnumType { get; init; } = "none";
        public List<Characteristics> Characteristics { get; init; } = new();
        public List<int> ProductTypes { get; init; } = new();
    }
}
