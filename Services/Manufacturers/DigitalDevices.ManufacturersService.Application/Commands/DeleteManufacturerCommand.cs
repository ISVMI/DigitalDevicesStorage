using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Commands
{
    public record DeleteManufacturerCommand(int Id) : IRequest<bool>;
}
