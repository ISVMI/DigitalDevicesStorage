using AutoMapper;
using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    public class EditManufacturerHandler : IRequestHandler<EditManufacturerCommand, ManufacturerDto>    
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public EditManufacturerHandler(IManufacturersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ManufacturerDto> Handle(EditManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = _mapper.Map<Manufacturer>(request.ManufacturerToEdit);
            await _repo.UpdateAsync(manufacturer, cancellationToken);
            return _mapper.Map<ManufacturerDto>(manufacturer);
        }
    }
}
