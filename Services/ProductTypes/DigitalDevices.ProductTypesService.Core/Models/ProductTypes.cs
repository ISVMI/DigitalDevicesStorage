using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Core.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public List<int> ProductsIds { get; set; } = new();
        public List<int> CharacteristicsTypesIds { get; set; } = new();

    }
}