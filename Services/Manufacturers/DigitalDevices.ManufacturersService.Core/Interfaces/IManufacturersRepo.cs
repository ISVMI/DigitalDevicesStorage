using DigitalDevices.ManufacturersService.Core.Models;

namespace DigitalDevices.ManufacturersService.Core.Interfaces
{
    public interface IManufacturersRepo
    {
        Task<Manufacturer> CreateAsync(Manufacturer manufacturer, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<Manufacturer> UpdateAsync(Manufacturer manufacturer, CancellationToken token = default);
        Task<Manufacturer> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken token = default);
    }
}