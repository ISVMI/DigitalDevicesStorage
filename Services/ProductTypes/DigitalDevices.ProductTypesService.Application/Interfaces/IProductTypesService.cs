using DigitalDevices.ProductTypesService.Application.Dtos;

namespace DigitalDevices.ProductTypesService.Application.Interfaces
{
    public interface IProductTypesService
    {
        Task<Guid> CreateAsync(CreateProductTypeDto createProductTypeDto, CancellationToken token = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
        Task<ProductTypeDto> UpdateAsync(EditProductTypeDto editProductTypeDto, CancellationToken token = default);
        Task<ProductTypeDto> GetByIdAsync(Guid id, CancellationToken token = default);
        Task<IEnumerable<ProductTypeDto>> GetAllAsync(CancellationToken token = default);
    }
}
