namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record CharacteristicDto
    {
        public string CharacteristicTypeName { get; init; }
        public string Value { get; init; }
    }
}
