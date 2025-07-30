using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Repositories
{
    public class CharacteristicsTypeRepo : ICharacteristicsTypeRepo
    {
        private readonly CharacteristicsContext _context;

        public CharacteristicsTypeRepo(CharacteristicsContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CharacteristicsType characteristicType, CancellationToken token = default)
        {
            if (characteristicType == null)
            {
                throw new ArgumentNullException(nameof(characteristicType), "Given characteristic type was null!");
            }

            if (await _context.CharacteristicsType
                    .AnyAsync(c => c.Name == characteristicType.Name, token))
            {
                throw new InvalidOperationException("Such characteristic type already exists!");
            }

            _context.CharacteristicsType.Add(characteristicType);
            await _context.SaveChangesAsync(token);
            return characteristicType.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var characteristicToDelete = await GetByIdAsync(id, token);

                _context.CharacteristicsType.Remove(characteristicToDelete);

                await _context.SaveChangesAsync(token);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't delete characteristic type with the given id {id} : {ex.Message}");
                return false;
            }
        }

        public async Task<CharacteristicsType> UpdateAsync(CharacteristicsType characteristicType, CancellationToken token = default)
        {
            try
            {
                var existingCharacteristic = await GetByIdAsync(characteristicType.Id, token);

                _context.Entry(existingCharacteristic).CurrentValues.SetValues(characteristicType);

                await _context.SaveChangesAsync(token);
                return existingCharacteristic;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update characteristic type {characteristicType.Name}", ex);
            }
        }

        public async Task<CharacteristicsType> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var characteristicTypeToFind = await _context.CharacteristicsType.FindAsync(id, token);

            if (characteristicTypeToFind == null)
            {
                throw new NotFoundException($"Characteristic type with id: {id} not found");
            }

            return characteristicTypeToFind;
        }

        public async Task<IEnumerable<CharacteristicsType>> GetAllAsync(CancellationToken token = default)
        {
            var characteristicsTypes = await _context.CharacteristicsType.ToListAsync(token);

            return characteristicsTypes;
        }
        public async Task<(IEnumerable<CharacteristicsType>, int)> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            var query = _context.CharacteristicsType.AsNoTracking();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            var totalCount = await query.CountAsync(token);

            return (items, totalCount);
        }
    }
}
