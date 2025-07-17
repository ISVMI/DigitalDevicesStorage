
namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record CreateCharacteristicDto
    {
        public string Value { get; init; }
        public int CharacteristicsTypeId { get; init; }
    }
}
