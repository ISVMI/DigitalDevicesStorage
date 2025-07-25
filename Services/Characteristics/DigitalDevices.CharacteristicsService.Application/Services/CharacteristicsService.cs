using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Application.Services
{
    public class CharacteristicsService : ICharacteristicsService
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public CharacteristicsService(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateCharacteristicDto createCharacteristicDto, CancellationToken token = default)
        {
            var newCharacteristic = _mapper.Map<Characteristics>(createCharacteristicDto);
            await _repo.CreateAsync(newCharacteristic, token);
            return newCharacteristic.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            return await _repo.DeleteAsync(id, token);
        }

        public async Task<CharacteristicDto> UpdateAsync(EditCharacteristicDto editCharacteristicDto, CancellationToken token = default)
        {
            var characteristicToEdit = _mapper.Map<Characteristics>(editCharacteristicDto);
            await _repo.UpdateAsync(characteristicToEdit, token);
            return _mapper.Map<CharacteristicDto>(characteristicToEdit);
        }

        public async Task<CharacteristicDto> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var characteristicToFind = await _repo.GetByIdAsync(id, token);
            return _mapper.Map<CharacteristicDto>(characteristicToFind);
        }

        public async Task<IEnumerable<CharacteristicDto>> GetAllAsync(CancellationToken token = default)
        {
            var characteristics = await _repo.GetAllAsync(token);
            return _mapper.Map<IEnumerable<CharacteristicDto>>(characteristics);
        }
    }
}
