using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetAllCharacteristicsQuery : IRequest<IEnumerable<Characteristics>>;
}
