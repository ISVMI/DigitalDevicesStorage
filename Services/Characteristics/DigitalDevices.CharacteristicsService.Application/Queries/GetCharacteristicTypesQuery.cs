using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicTypesQuery(int Id) : IRequest<CharacteristicsType>;
}
