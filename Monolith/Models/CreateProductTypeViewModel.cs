using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class CreateProductTypeViewModel
    {
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Типы характеристик")]
        public List<string> CharacteristicsTypesNames { get; set; } = new();
    }
}
