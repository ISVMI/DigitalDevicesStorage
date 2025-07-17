namespace DigitalDevices.Models
{
    public class CharacteristicsProduct
    {
        public int CharacteristicsId { get; set; }
        public int ProductId { get; set; }
        public virtual Characteristics Characteristics { get; set; }
        public virtual Product Products { get; set; }
    }
}
