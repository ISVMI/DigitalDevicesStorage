
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Core.Interfaces
{
    public interface ICharacteristicsTypeRepo
    {
        Task<CharacteristicsType> CreateAsync(CharacteristicsType characteristicType, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<CharacteristicsType> UpdateAsync(CharacteristicsType characteristicType, CancellationToken token = default);
        Task<CharacteristicsType> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<CharacteristicsType>> GetAllAsync(CancellationToken token = default);
    }
}
