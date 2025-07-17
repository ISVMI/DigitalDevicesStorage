using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Commands
{
    public record CreateManufacturerCommand(Manufacturer Manufacturer) : IRequest<Manufacturer>;
}
