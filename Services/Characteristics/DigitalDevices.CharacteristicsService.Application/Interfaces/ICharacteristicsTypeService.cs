using DigitalDevices.CharacteristicsService.Application.Dtos;

namespace DigitalDevices.CharacteristicsService.Application.Interfaces
{
    public interface ICharacteristicsTypeService
    {
        Task<CharacteristicTypeDto> CreateAsync(CreateCharacteristicTypeDto createCharacteristicTypeDto, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<CharacteristicTypeDto> UpdateAsync(EditCharacteristicTypeDto editCharacteristicTypeDto, CancellationToken token = default);
        Task<CharacteristicTypeDto> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<CharacteristicTypeDto>> GetAllAsync(CancellationToken token = default);
    }
}
