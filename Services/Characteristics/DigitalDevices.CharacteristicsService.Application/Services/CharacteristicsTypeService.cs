using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Application.Services
{
    public class CharacteristicsTypeService : ICharacteristicsTypeService
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public CharacteristicsTypeService(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateCharacteristicTypeDto createCharacteristicTypeDto, CancellationToken token = default)
        {
            var characteristicType = _mapper.Map<CharacteristicsType>(createCharacteristicTypeDto);
            await _repo.CreateAsync(characteristicType, token);
            return characteristicType.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            return await _repo.DeleteAsync(id, token);
        }

        public async Task<CharacteristicTypeDto> UpdateAsync(EditCharacteristicTypeDto editCharacteristicTypeDto, CancellationToken token = default)
        {
            var characteristicTypeToEdit = _mapper.Map<CharacteristicsType>(editCharacteristicTypeDto);
            await _repo.UpdateAsync(characteristicTypeToEdit, token);
            return _mapper.Map<CharacteristicTypeDto>(characteristicTypeToEdit);
        }

        public async Task<CharacteristicTypeDto> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var characteristicTypeToFind = await _repo.GetByIdAsync(id, token);
            return _mapper.Map<CharacteristicTypeDto>(characteristicTypeToFind);
        }

        public async Task<IEnumerable<CharacteristicTypeDto>> GetAllAsync(CancellationToken token = default)
        {
            var characteristicsTypes = await _repo.GetAllAsync(token);
            return _mapper.Map<IEnumerable<CharacteristicTypeDto>>(characteristicsTypes);
        }

    }
}
