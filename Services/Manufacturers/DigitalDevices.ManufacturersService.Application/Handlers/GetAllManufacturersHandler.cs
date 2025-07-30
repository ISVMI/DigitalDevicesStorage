using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Queries;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetAllManufacturersHandler : IRequestHandler<GetAllManufacturersQuery, IEnumerable<ManufacturerDto>>
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public GetAllManufacturersHandler(IManufacturersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManufacturerDto>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
        {
            var manufacturers = await _repo.GetAllAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers);
            return result;
        }
    }
}
