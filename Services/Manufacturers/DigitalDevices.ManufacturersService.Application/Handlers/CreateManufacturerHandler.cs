using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class CreateManufacturerHandler : IRequestHandler<CreateManufacturerCommand, Guid>
    {
        private readonly IManufacturersService _service;

        public CreateManufacturerHandler(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CreateAsync(request.Manufacturer, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new manufacturer: {ex.Message}";
                Console.WriteLine(message);
            }

            return Guid.Empty;
        }
    }
}