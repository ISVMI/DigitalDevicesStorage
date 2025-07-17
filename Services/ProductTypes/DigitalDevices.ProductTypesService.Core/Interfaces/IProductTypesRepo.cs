using DigitalDevices.ProductTypesService.Core.Models;

namespace DigitalDevices.ProductTypesService.Core.Interfaces
{
    public interface IProductTypesRepo
    {
        Task<ProductTypes> CreateAsync(ProductTypes productType, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<ProductTypes> UpdateAsync(ProductTypes productType, CancellationToken token = default);
        Task<ProductTypes> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<ProductTypes>> GetAllAsync(CancellationToken token = default);
    }
}
