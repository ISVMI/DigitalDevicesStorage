using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicQuery(Guid Id) : IRequest<CharacteristicDto>;
}
