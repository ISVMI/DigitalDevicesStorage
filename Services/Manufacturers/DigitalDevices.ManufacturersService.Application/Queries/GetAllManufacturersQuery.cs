using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Queries
{
    public record GetAllManufacturersQuery : IRequest<IEnumerable<Manufacturer>>;
}
