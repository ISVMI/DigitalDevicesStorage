using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicQuery(int Id) : IRequest<Characteristics>;
}
