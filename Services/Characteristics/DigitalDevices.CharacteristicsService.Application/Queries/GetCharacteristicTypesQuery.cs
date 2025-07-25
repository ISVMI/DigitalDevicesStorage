using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicTypesQuery(Guid Id) : IRequest<CharacteristicTypeDto>;
}
