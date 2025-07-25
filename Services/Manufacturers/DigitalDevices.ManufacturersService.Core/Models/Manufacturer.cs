using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ManufacturersService.Core.Models
{
    public class Manufacturer
    {
        [Key]
        public Guid Id { get;}
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}