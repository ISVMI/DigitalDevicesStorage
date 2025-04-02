using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public List<CharacteristicsTypesModel> CharacteristicsTypes { get; set; } = new();
    
    public class CharacteristicsTypesModel
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Тип данных")]
        public string DataType { get; set; }
    }
    }
}
