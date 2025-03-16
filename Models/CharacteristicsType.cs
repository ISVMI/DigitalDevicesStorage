
namespace DigitalDevices.Models
{
    public class CharacteristicsType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string EnumType { get; set; } = "none";
        public List<Characteristics> Characteristics { get; set; } = new ();
        public List<CharacteristicsTypeProductTypes> CharacteristicsTypeProductTypes { get; set; } = new();
        public CharacteristicsType()
        {
            
        }
    }
}
