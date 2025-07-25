using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetAllCharacteristicsQuery : IRequest<IEnumerable<CharacteristicDto>>;
}
