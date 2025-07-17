using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;

namespace DigitalDevices.ProductTypesService.Application.Services
{
    public class ProductTypesService : IProductTypesService
    {
        private readonly IProductTypesRepo _repo;
        private readonly IMapper _mapper;

        public ProductTypesService(IProductTypesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductTypeDto> CreateAsync(CreateProductTypeDto createProductTypeDto, CancellationToken token = default)
        {
            var productType = _mapper.Map<ProductTypes>(createProductTypeDto);
            await _repo.CreateAsync(productType, token);
            return _mapper.Map<ProductTypeDto>(productType);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
        {
            return await _repo.DeleteAsync(id, token);
        }

        public async Task<ProductTypeDto> UpdateAsync(EditProductTypeDto editProductTypeDto, CancellationToken token = default)
        {
            var productTypeToEdit = _mapper.Map<ProductTypes>(editProductTypeDto);
            await _repo.UpdateAsync(productTypeToEdit, token);
            return _mapper.Map<ProductTypeDto>(productTypeToEdit);
        }

        public async Task<ProductTypeDto> GetByIdAsync(int id, CancellationToken token = default)
        {
            var productTypeToFind = await _repo.GetByIdAsync(id, token);
            return _mapper.Map<ProductTypeDto>(productTypeToFind);
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllAsync(CancellationToken token = default)
        {
            var productTypes = await _repo.GetAllAsync(token);
            return _mapper.Map<IEnumerable<ProductTypeDto>>(productTypes);
        }
    }
}