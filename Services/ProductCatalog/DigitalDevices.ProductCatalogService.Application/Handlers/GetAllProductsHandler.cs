using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Queries;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductsByTypeDto>>
    {
        private readonly IProductCatalogRepo _repo;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductCatalogRepo repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ProductsByTypeDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
            var productTypes = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductsByTypeDto>>(productTypes);
        }
    }
}
