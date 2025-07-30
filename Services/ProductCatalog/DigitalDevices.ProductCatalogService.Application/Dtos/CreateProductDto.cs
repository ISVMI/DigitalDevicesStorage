using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public class CreateProductDto
    {
        [Display(Name = "Цена")]
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
        public Guid ProductTypesId { get; set; }

        public List<CharacteristicInput> Characteristics { get; set; } = new();
    }

    public class CharacteristicInput
    {
        public Guid CharacteristicTypeId { get; set; }
        public string Value { get; set; }
    }
}
