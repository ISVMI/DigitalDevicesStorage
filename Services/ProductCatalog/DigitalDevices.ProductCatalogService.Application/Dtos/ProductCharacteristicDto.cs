using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDevices.ProductCatalogService.Application.Dtos
{
    public class ProductCharacteristicDto
    {
        public Guid CharacteristicTypeId { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string DataType { get; set; }
        [NotMapped]
        public Dictionary<string, string> EnumValues { get; set; }
    }
}
