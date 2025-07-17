using DigitalDevices.ManufacturersService.Application.Queries;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetManufacturerHandler : IRequestHandler<GetManufacturerQuery, Manufacturer>
    {
        private readonly IManufacturersRepo _repo;

        public GetManufacturerHandler(IManufacturersRepo repo)
        {
            _repo = repo;
        }

        public async Task<Manufacturer> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
