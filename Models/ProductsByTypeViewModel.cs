using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class ProductsByTypeViewModel
    {
      public int Id { get; set; }
    [Display(Name = "Цена")]
    [Range(10, 1_000_000), DataType(DataType.Currency)]
    [Column(TypeName = "float(18, 2)")]
    public decimal Price { get; set; }
    [Display(Name = "Наименование")]
    public string Name { get; set; }
    [Display(Name = "Модель")]
    public string Model { get; set; }
    [Display(Name = "Цвет")]
    public string Color { get; set; }
    [Display(Name = "Срок гарантии")]
    public int Warranty { get; set; }
    [Display(Name = "Производитель")]
    public Manufacturer Manufacturer { get; set; }

    [Display(Name = "Тип продукта")]
    public ProductTypes ProductType { get; set; }

    public List<CharacteristicByType> Characteristics { get; set; } = new();

    public class CharacteristicByType
    {
        public string CharacteristicType { get; set; }
        public string Value { get; set; }
    }
}
}
