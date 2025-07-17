using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record CharacteristicTypeDto
    {
        [Display(Name = "Наименование")]
        public string Name { get; init; }
        [Display(Name = "Тип данных")]
        public string DataType { get; init; }
        [Display(Name = "Тип перечисления")]
        public string EnumType { get; init; } = "none";
    }
}
