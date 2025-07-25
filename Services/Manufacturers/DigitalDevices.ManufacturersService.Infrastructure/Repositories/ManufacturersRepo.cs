using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Core.Models;
using DigitalDevices.ManufacturersService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.ManufacturersService.Infrastructure.Repositories
{
    public class ManufacturersRepo : IManufacturersRepo
    {
        private readonly ManufacturersContext _context;

        public ManufacturersRepo(ManufacturersContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(Manufacturer manufacturer, CancellationToken token = default)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException(nameof(manufacturer), "Given manufacturer was null!");
            }

            if (await _context.Manufacturers
                    .AnyAsync(m => m.Name == manufacturer.Name, token))
            {
                throw new InvalidOperationException("Manufacturer already exists!");
            }

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync(token);
            return manufacturer.Id;

        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            try
            {
                var manufacturerToDelete = await GetByIdAsync(id, token);

                _context.Manufacturers.Remove(manufacturerToDelete);

                await _context.SaveChangesAsync(token);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Couldn't delete manufacturer with the given id {id} : {ex.Message}");
                return false;
            }
        }

        public async Task<Manufacturer> UpdateAsync(Manufacturer manufacturer, CancellationToken token = default)
        {
            try
            {
                var existingManufacturer = await GetByIdAsync(manufacturer.Id, token);

                _context.Entry(existingManufacturer).CurrentValues.SetValues(manufacturer);

                await _context.SaveChangesAsync(token);
                return existingManufacturer;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update manufacturer {manufacturer.Name}", ex);
            }
        }

        public async Task<Manufacturer> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var manufacturerToFind = await _context.Manufacturers.FindAsync(id, token);

            if (manufacturerToFind == null)
            {
                throw new Exception($"Manufacturer with id: {id} not found");
            }

            return manufacturerToFind;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken token = default)
        {
            var manufacturers = await _context.Manufacturers.ToListAsync(token);

            return manufacturers;
        }
    }
}
