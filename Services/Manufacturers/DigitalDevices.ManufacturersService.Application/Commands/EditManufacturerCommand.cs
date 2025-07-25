using DigitalDevices.ManufacturersService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Commands
{
    public record EditManufacturerCommand(EditManufacturerDto ManufacturerToEdit) : IRequest<ManufacturerDto>;
}
