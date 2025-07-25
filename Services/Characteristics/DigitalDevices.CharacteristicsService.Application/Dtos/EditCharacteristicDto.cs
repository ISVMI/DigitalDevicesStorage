
namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record EditCharacteristicDto
    {
        public Guid Id { get; init; }
        public string Value { get; init; }
        public int CharacteristicsTypeId { get; init; }
    }
}
