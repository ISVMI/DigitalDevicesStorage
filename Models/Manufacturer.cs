using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public Manufacturer()
        {
            
        }
    }
}
