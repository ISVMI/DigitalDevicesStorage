using DigitalDevices.ManufacturersService.Application.Queries;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetAllManufacturersHandler : IRequestHandler<GetAllManufacturersQuery, IEnumerable<Manufacturer>>
    {
        private readonly IManufacturersRepo _repo;

        public GetAllManufacturersHandler(IManufacturersRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Manufacturer>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}
