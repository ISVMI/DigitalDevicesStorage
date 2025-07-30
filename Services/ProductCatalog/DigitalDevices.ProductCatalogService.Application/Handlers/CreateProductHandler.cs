using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Commands;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using DigitalDevices.ProductCatalogService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductCatalogRepo _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductCatalogRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productType = _mapper.Map<Product>(request.Product);
            await _repo.CreateAsync(productType, cancellationToken);
            return productType.Id;
        }
    }
}
