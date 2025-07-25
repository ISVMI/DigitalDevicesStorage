using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class EditManufacturerHandler : IRequestHandler<EditManufacturerCommand, ManufacturerDto>    
    {
        private readonly IManufacturersService _service;

        public EditManufacturerHandler(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<ManufacturerDto> Handle(EditManufacturerCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.ManufacturerToEdit, cancellationToken);
        }
    }
}
