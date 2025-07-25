
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Core.Interfaces
{
    public interface ICharacteristicsTypeRepo
    {
        Task<Guid> CreateAsync(CharacteristicsType characteristicType, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<CharacteristicsType> UpdateAsync(CharacteristicsType characteristicType, CancellationToken token = default);
        Task<CharacteristicsType> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<CharacteristicsType>> GetAllAsync(CancellationToken token = default);
    }
}
