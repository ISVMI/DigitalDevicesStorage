using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetCharacteristicsByProductTypeIdQuery(Guid ProductTypeId) : IRequest<IEnumerable<CharacteristicDto>>
    {
    }
}
