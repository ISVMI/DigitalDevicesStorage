using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicTypeQuery(Guid Id) : IRequest<CharacteristicTypeDto>;
}
