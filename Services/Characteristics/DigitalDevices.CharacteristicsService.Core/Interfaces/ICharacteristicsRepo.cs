using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalDevices.CharacteristicsService.Core.Models;

namespace DigitalDevices.CharacteristicsService.Core.Interfaces
{
    public interface ICharacteristicsRepo
    {
        Task<Guid> CreateAsync(Characteristics characteristic, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<Characteristics> UpdateAsync(Characteristics characteristic, CancellationToken token = default);
        Task<Characteristics> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Characteristics>> GetAllAsync(CancellationToken token = default);
    }
}
