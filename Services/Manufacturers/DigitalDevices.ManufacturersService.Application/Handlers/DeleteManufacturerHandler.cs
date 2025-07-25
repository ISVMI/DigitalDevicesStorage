using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    internal class DeleteManufacturerHandler : IRequestHandler<DeleteManufacturerCommand, bool>
    {
        private readonly IManufacturersService _service;

        public DeleteManufacturerHandler(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
