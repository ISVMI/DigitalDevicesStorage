using DigitalDevices.ProductTypesService.Core.Models;

namespace DigitalDevices.ProductTypesService.Core.Interfaces
{
    public interface IProductTypesRepo
    {
        Task<Guid> CreateAsync(ProductTypes productType, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<ProductTypes> UpdateAsync(ProductTypes productType, CancellationToken token = default);
        Task<ProductTypes> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<ProductTypes>> GetAllAsync(CancellationToken token = default);
    }
}
