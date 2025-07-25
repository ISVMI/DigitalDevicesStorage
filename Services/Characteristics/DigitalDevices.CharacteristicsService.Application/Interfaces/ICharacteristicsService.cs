using DigitalDevices.CharacteristicsService.Application.Dtos;

namespace DigitalDevices.CharacteristicsService.Application.Interfaces
{
    public interface ICharacteristicsService
    {
        Task<Guid> CreateAsync(CreateCharacteristicDto createCharacteristicDto, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<CharacteristicDto> UpdateAsync(EditCharacteristicDto editCharacteristicDto, CancellationToken token = default);
        Task<CharacteristicDto> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<CharacteristicDto>> GetAllAsync(CancellationToken token = default);
    }
}
