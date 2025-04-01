using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDevices.Models
{
    public class EditProductViewModel
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
        public int ManufacturerId { get; set; }

        [Display(Name = "Тип продукта")]
        public int ProductTypeId { get; set; }

        public List<ProductCharacteristicEditVM> Characteristics { get; set; } = new ();

        public class ProductCharacteristicEditVM
        {
            public int CharacteristicTypeId { get; set; }
            public string Value { get; set; }
            public string Name { get; set; } = "none";
            public string DataType { get; set; } = "string";
            public Dictionary<string, string> EnumValues { get; set; }
        }
    }
}
