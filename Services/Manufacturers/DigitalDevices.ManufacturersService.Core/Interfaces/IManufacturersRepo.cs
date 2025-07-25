using DigitalDevices.ManufacturersService.Core.Models;

namespace DigitalDevices.ManufacturersService.Core.Interfaces
{
    public interface IManufacturersRepo
    {
        Task<Guid> CreateAsync(Manufacturer manufacturer, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<Manufacturer> UpdateAsync(Manufacturer manufacturer, CancellationToken token = default);
        Task<Manufacturer> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<Manufacturer>> GetAllAsync(CancellationToken token = default);
    }
}