using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Application.Dtos
{
    public record CharacteristicDto
    {
        public string Value { get; init; }

        public string CharacteristicTypeName { get; init; }
    }
}
