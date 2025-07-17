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
        Task<Characteristics> CreateAsync(Characteristics characteristic, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<Characteristics> UpdateAsync(Characteristics characteristic, CancellationToken token = default);
        Task<Characteristics> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<Characteristics>> GetAllAsync(CancellationToken token = default);
    }
}
