using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Queries;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class GetAllProductsPagedHandler : IRequestHandler<GetAllProductsPagedQuery, PagedResponse<ProductsByTypeDto>>
    {
        private readonly IProductCatalogRepo _repo;
        private readonly IMapper _mapper;

        public GetAllProductsPagedHandler(IProductCatalogRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductsByTypeDto>> Handle(GetAllProductsPagedQuery request, CancellationToken token)
        {
            var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize, token);

            return new PagedResponse<ProductsByTypeDto>(_mapper.Map<IEnumerable<ProductsByTypeDto>>(items), totalCount,
                request.Page, request.PageSize);
        }
    }
}
