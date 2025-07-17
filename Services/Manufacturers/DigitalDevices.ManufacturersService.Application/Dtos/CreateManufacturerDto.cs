using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.ManufacturersService.Application.Dtos
{
    public record CreateManufacturerDto
    {
        public string Name { get; init; }
        [Display(Name = "Страна")]
        public string Country { get; init; }
        [Display(Name = "Адрес")]
        public string Address { get; init; }
    }
}
