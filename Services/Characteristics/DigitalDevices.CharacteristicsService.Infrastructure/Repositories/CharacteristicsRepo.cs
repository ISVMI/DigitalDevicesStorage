using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using Microsoft.EntityFrameworkCore;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using Shared.Exceptions;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Repositories
{
    public class CharacteristicsRepo : ICharacteristicsRepo
    {
        private readonly CharacteristicsContext _context;

        public CharacteristicsRepo(CharacteristicsContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Characteristics characteristic, CancellationToken token = default)
        {
            if (characteristic == null)
            {
                throw new ArgumentNullException(nameof(characteristic), "Given characteristic was null!");
            }

            _context.Characteristics.Add(characteristic);

            var characteristicType = await _context.CharacteristicsType.FindAsync(characteristic.CharacteristicsTypeId);

            if (characteristicType != null)
            {
                characteristicType.Characteristics.Add(characteristic);
                characteristic.CharacteristicsType = characteristicType;
            }

            await _context.SaveChangesAsync(token);
            return characteristic.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var characteristicToDelete = await GetByIdAsync(id, token);

                _context.Characteristics.Remove(characteristicToDelete);

                await _context.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't delete characteristic with the given id {id} : {ex.Message}");
                return false;
            }
        }

        public async Task<Characteristics> UpdateAsync(Characteristics characteristic, CancellationToken token = default)
        {
            try
            {
                var existingCharacteristic = await GetByIdAsync(characteristic.Id, token);

                _context.Entry(existingCharacteristic).CurrentValues.SetValues(characteristic);

                await _context.SaveChangesAsync(token);
                return existingCharacteristic;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update characteristic", ex);
            }
        }

        public async Task<Characteristics> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var characteristicToFind = await _context.Characteristics.FindAsync(id, token);

            if (characteristicToFind == null)
            {
                throw new NotFoundException($"Characteristic with id: {id} not found");
            }

            return characteristicToFind;
        }

        public async Task<IEnumerable<Characteristics>> GetAllAsync(CancellationToken token = default)
        {
            var characteristics = await _context.Characteristics.ToListAsync(token);

            return characteristics;
        }

        public async Task<(IEnumerable<Characteristics>, int)> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            var query = _context.Characteristics.AsNoTracking();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var totalCount = await query.CountAsync(token);

            return (items, totalCount);
        }
    }
}
