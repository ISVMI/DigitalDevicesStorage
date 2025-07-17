
namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record EditCharacteristicDto
    {
        public int Id { get; init; }
        public string Value { get; init; }
        public int CharacteristicsTypeId { get; init; }
    }
}
