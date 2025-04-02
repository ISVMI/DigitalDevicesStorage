
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class CharacteristicsType
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Тип данных")]
        public string DataType { get; set; }
        [Display(Name = "Тип перечисления")]
        public string EnumType { get; set; } = "none";
        public List<Characteristics> Characteristics { get; set; } = new ();
        public List<CharacteristicsTypeProductTypes> CharacteristicsTypeProductTypes { get; set; } = new();

    }
}
