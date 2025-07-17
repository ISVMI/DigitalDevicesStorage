using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.CharacteristicsService.Core.Models
{
    public class Characteristics
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int CharacteristicsTypeId { get; set; }
        public virtual CharacteristicsType CharacteristicsType { get; set; }

    }
}
