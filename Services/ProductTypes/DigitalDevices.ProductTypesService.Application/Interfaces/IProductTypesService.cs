using DigitalDevices.ProductTypesService.Application.Dtos;

namespace DigitalDevices.ProductTypesService.Application.Interfaces
{
    public interface IProductTypesService
    {
        Task<ProductTypeDto> CreateAsync(CreateProductTypeDto createProductTypeDto, CancellationToken token = default);
        Task<bool> DeleteAsync(int id, CancellationToken token = default);
        Task<ProductTypeDto> UpdateAsync(EditProductTypeDto editProductTypeDto, CancellationToken token = default);
        Task<ProductTypeDto> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<ProductTypeDto>> GetAllAsync(CancellationToken token = default);
    }
}
