using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class ProductCharacteristicViewModel
    {
        public int CharacteristicTypeId { get; set; }
        public string Value { get; set; }
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string DataType { get; set; }
        [NotMapped]
        public Dictionary<string, string> EnumValues { get; set; }
    }
}
