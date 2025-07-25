using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using DigitalDevices.ManufacturersService.Application.Queries;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetAllManufacturersHandler : IRequestHandler<GetAllManufacturersQuery, IEnumerable<ManufacturerDto>>
    {
        private readonly IManufacturersService _service;

        public GetAllManufacturersHandler(IManufacturersService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<ManufacturerDto>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
