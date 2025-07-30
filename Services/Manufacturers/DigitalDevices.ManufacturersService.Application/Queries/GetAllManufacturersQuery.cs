using DigitalDevices.ManufacturersService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Queries
{
    public record GetAllManufacturersQuery : IRequest<IEnumerable<ManufacturerDto>>
    {
    }
}
