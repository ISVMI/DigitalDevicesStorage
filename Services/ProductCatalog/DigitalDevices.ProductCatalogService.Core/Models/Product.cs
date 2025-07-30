using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDevices.ProductCatalogService.Core.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
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
        public Guid ManufacturerId { get; set; }
        [Display(Name = "Тип продукта")]
        public Guid ProductTypesId { get; set; }
    }
}
