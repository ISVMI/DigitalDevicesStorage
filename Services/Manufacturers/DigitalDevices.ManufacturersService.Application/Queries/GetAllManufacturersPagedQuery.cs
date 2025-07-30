using DigitalDevices.ManufacturersService.Application.Dtos;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ManufacturersService.Application.Queries
{
    public record GetAllManufacturersPagedQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResponse<ManufacturerDto>>;
}
