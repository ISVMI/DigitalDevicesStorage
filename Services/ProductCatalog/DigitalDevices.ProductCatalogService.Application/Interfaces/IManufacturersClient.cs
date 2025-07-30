using DigitalDevices.ProductCatalogService.Application.Dtos;

namespace DigitalDevices.ProductCatalogService.Application.Interfaces
{
    public interface IManufacturersClient
    {
        Task<IEnumerable<ManufacturerDto>> GetAllAsync(CancellationToken token = default);
    }
}
