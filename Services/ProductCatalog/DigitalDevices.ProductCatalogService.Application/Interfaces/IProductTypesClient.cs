using DigitalDevices.ProductCatalogService.Application.Dtos;

namespace DigitalDevices.ProductCatalogService.Application.Interfaces
{
    public interface IProductTypesClient
    {
        Task<IEnumerable<ProductTypeDto>> GetAllAsync(CancellationToken token = default);
    }
}
