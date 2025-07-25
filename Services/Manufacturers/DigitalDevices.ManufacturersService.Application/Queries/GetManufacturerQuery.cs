using DigitalDevices.ManufacturersService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Queries
{
    public record GetManufacturerQuery(Guid Id) : IRequest<ManufacturerDto>;
}
