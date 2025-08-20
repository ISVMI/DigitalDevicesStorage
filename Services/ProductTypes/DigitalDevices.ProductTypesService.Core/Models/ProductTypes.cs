using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Core.Models
{
    public class ProductTypes
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public List<Guid> ProductsIds { get; set; } = new();

    }
}