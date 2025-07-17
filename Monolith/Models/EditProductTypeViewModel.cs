using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class EditProductTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Добавить характеристики:")]
        public List<string> CharacteristicTypesToAdd { get; set; } = new();
        [Display(Name = "Удалить характеристики:")]
        public List<string> CharacteristicTypesToDelete { get; set; } = new();
    }
}
