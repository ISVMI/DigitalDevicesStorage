using AutoMapper;
using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Queries;
using DigitalDevices.ProductCatalogService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, ProductsByTypeDto>

    {
    private readonly IProductCatalogRepo _repo;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductCatalogRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductsByTypeDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productTypeToFind = await _repo.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<ProductsByTypeDto>(productTypeToFind);
        }
    }
}
