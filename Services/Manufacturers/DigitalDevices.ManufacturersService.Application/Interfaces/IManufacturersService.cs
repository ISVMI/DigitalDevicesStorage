using DigitalDevices.ManufacturersService.Application.Dtos;
using DigitalDevices.ManufacturersService.Core.Models;

namespace DigitalDevices.ManufacturersService.Application.Interfaces
{
    public interface IManufacturersService
    {
        Task<ManufacturerDto> CreateAsync(CreateManufacturerDto manufacturerDto, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<ManufacturerDto> UpdateAsync(EditManufacturerDto manufacturerDto, CancellationToken token = default);
        Task<ManufacturerDto> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<ManufacturerDto>> GetAllAsync(CancellationToken token = default);
    }
}
