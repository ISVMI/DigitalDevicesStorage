using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new ();
        public List<CharacteristicsTypeProductTypes> CharacteristicsTypeProductTypes { get; set; } = new();

    }
}