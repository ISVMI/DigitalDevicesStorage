using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Commands;
using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using DigitalDevices.ProductCatalogService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class EditProductHandler : IRequestHandler<EditProductCommand, ProductsByTypeDto>
    {
        private readonly IProductCatalogRepo _repo;
        private readonly IMapper _mapper;

        public EditProductHandler(IProductCatalogRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductsByTypeDto> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var productType = _mapper.Map<Product>(request.ProductToEdit);
            await _repo.UpdateAsync(productType, cancellationToken);
            return _mapper.Map<ProductsByTypeDto>(productType);
        }
    }
}
