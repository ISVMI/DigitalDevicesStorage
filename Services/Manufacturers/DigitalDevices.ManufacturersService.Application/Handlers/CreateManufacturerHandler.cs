using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class CreateManufacturerHandler : IRequestHandler<CreateManufacturerCommand, Guid>
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public CreateManufacturerHandler(IManufacturersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var manufacturer = _mapper.Map<Manufacturer>(request.Manufacturer);
                var result = await _repo.CreateAsync(manufacturer, cancellationToken);
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