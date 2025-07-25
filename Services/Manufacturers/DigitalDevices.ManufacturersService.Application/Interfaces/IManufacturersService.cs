using DigitalDevices.ManufacturersService.Application.Dtos;

namespace DigitalDevices.ManufacturersService.Application.Interfaces
{
    public interface IManufacturersService
    {
        Task<Guid> CreateAsync(CreateManufacturerDto manufacturerDto, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<ManufacturerDto> UpdateAsync(EditManufacturerDto manufacturerDto, CancellationToken token = default);
        Task<ManufacturerDto> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<ManufacturerDto>> GetAllAsync(CancellationToken token = default);
    }
}
