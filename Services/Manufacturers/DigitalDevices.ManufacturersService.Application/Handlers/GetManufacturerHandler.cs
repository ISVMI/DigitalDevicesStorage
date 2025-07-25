using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using DigitalDevices.ManufacturersService.Application.Queries;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetManufacturerHandler : IRequestHandler<GetManufacturerQuery, ManufacturerDto>
    {
        private readonly IManufacturersService _service;

        public GetManufacturerHandler(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<ManufacturerDto> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetByIdAsync(request.Id, cancellationToken);
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
