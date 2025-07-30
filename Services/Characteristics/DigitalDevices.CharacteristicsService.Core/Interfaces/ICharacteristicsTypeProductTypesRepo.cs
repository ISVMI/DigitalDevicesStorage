using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Core.Interfaces
{
    public interface ICharacteristicsTypeProductTypesRepo
    {
        Task<IEnumerable<CharacteristicsType>> GetByProductTypeId(Guid productTypeId, CancellationToken token = default);
    }
}
