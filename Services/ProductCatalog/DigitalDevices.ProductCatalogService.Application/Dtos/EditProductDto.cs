using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public class EditProductDto
    {
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
        [Display(Name = "Срок гарантии")]
        public int Warranty { get; set; }
        [Display(Name = "Производитель")]
        public Guid ManufacturerId { get; set; }

        [Display(Name = "Тип продукта")]
        public Guid ProductTypeId { get; set; }

        public List<ProductCharacteristicEditDto> Characteristics { get; set; } = new();

        public class ProductCharacteristicEditDto
        {
            public Guid CharacteristicTypeId { get; set; }
            public string Value { get; set; }
            public string Name { get; set; } = "none";
            public string DataType { get; set; } = "string";
            public Dictionary<string, string> EnumValues { get; set; }
        }
    }
}
