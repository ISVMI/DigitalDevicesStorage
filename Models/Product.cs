using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalDevices.Enums;
namespace DigitalDevices.Models
{
    public class Product
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
        [Display(Name = "Срок гарантии мес.")]
        public int Warranty { get; set; }
        [Display(Name = "Производитель")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Производитель")]
        public virtual Manufacturer Manufacturer { get; set; }
        [Display(Name = "Тип продукта")]
        public int ProductTypesId { get; set; }
        [Display(Name = "Тип продукта")]
        public virtual ProductTypes ProductTypes { get; set; }
        public List<CharacteristicsProduct> CharacteristicsProduct { get; set; } = new();

        
    }
}
