using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.CharacteristicsService.Application.Queries
{
    public record GetAllCharacteristicsPagedQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResponse<CharacteristicDto>>;
}
