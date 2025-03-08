using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Display(Name = "������������")]
        public string Name { get; set; }
        [Display(Name = "������")]
        public string Country { get; set; }
        [Display(Name = "�����")]
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public Manufacturer()
        {
            
        }
    }
}
