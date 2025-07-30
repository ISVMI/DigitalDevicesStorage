using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Queries;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class GetManufacturerHandler : IRequestHandler<GetManufacturerQuery, ManufacturerDto>
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public GetManufacturerHandler(IManufacturersRepo repo, IMapper mapper)
        {
           _repo = repo;
           _mapper = mapper;
        }

        public async Task<ManufacturerDto> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var manufacturer = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<ManufacturerDto>(manufacturer);
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
