using DigitalDevices.CharacteristicsService.Application.Dtos;

namespace DigitalDevices.CharacteristicsService.Application.Interfaces
{
    public interface ICharacteristicsService
    {
        Task<CharacteristicDto> CreateAsync(CreateCharacteristicDto createCharacteristicDto, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<CharacteristicDto> UpdateAsync(EditCharacteristicDto editCharacteristicDto, CancellationToken token = default);
        Task<CharacteristicDto> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<CharacteristicDto>> GetAllAsync(CancellationToken token = default);
    }
}
