using DigitalDevices.Enums;
using System.ComponentModel.DataAnnotations;

namespace DigitalDevices.Models
{
    public class Characteristics
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int CharacteristicsTypeId { get; set; }
        public virtual CharacteristicsType CharacteristicsType { get; set; }
        public List<CharacteristicsProduct> CharacteristicsProduct { get; set; } = new();
        public Characteristics()
        {
            
        }
    }
}
