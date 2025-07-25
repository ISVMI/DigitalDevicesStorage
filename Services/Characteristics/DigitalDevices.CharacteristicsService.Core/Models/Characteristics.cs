using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.CharacteristicsService.Core.Models
{
    public class Characteristics
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid CharacteristicsTypeId { get; set; }
        public virtual CharacteristicsType CharacteristicsType { get; set; }

    }
}
