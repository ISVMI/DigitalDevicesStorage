using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Цена")]
        public float Price { get; set; }
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
        public int ProductTypesId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public ProductTypes ProductTypes { get; set; }
        public List<Characteristics> Characteristics { get; set; } = new();
        public List<CharacteristicsType> CharacteristicsTypes { get; set; } = new();
    }
}
