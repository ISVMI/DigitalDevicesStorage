using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using AutoMapper;

namespace DigitalDevices.ManufacturersService.Application.Services
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly IManufacturersRepo _repo;
        private readonly IMapper _mapper;

        public ManufacturersService(IManufacturersRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateManufacturerDto manufacturerDto, CancellationToken token = default)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            var result = await _repo.CreateAsync(manufacturer, token);
            return manufacturer.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            return await _repo.DeleteAsync(id, token);
        }

        public async Task<ManufacturerDto> UpdateAsync(EditManufacturerDto manufacturerDto, CancellationToken token = default)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            await _repo.UpdateAsync(manufacturer, token);
            return _mapper.Map<ManufacturerDto>(manufacturer);
        }

        public async Task<ManufacturerDto> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var manufacturer = await _repo.GetByIdAsync(id, token);
            return _mapper.Map<ManufacturerDto>(manufacturer);
        }

        public async Task<IEnumerable<ManufacturerDto>> GetAllAsync(CancellationToken token = default)
        {
            var result = await _repo.GetAllAsync(token);
            return _mapper.Map<IEnumerable<ManufacturerDto>>(result);
        }
    }
}
