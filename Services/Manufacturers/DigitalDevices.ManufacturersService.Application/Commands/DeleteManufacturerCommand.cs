using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Commands
{
    public record DeleteManufacturerCommand(Guid Id) : IRequest<bool>;
}
