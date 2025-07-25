using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ProductTypesService.Core.Models
{
    public class ProductTypes
    {
        public Guid Id { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public List<Guid> ProductsIds { get; set; } = new();

    }
}