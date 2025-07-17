using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class CreateManufacturerHandler : IRequestHandler<CreateManufacturerCommand, Manufacturer>
    {
        private readonly IManufacturersRepo _repo;

        public CreateManufacturerHandler(IManufacturersRepo repo)
        {
            _repo = repo;
        }

        public async Task<Manufacturer> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CreateAsync(request.Manufacturer, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}