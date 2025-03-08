using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public abstract class Product
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
        [Display(Name = "Производитель")]
        public virtual Manufacturer Manufacturer { get; set; }
        protected Product()
        {
            
        }
    }
}
