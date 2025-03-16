using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DigitalDevices.Models
{
    public class CreateProductViewModel
    {
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
        [BindProperty]
        public List<CharacteristicInput> Characteristics { get; set; }
        public SelectList Manufacturers { get; set; }
        public SelectList ProductTypes { get; set; }
    }

    public class CharacteristicInput
    {
        public int CharacteristicTypeId { get; set; }
        public string Value { get; set; }
    }
}
