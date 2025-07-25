using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.CharacteristicsService.Core.Models
{
    public class CharacteristicsType
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Тип данных")]
        public string DataType { get; set; }
        [Display(Name = "Тип перечисления")]
        public string EnumType { get; set; } = "none";
        public List<Characteristics> Characteristics { get; set; } = new();

    }
}
