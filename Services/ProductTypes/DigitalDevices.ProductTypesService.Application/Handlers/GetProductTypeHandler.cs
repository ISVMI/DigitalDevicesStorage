using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Queries;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetProductTypeHandler : IRequestHandler<GetProductTypeQuery, ProductTypeDto>

    {
    private readonly IProductTypesRepo _repo;
    private readonly IMapper _mapper;

    public GetProductTypeHandler(IProductTypesRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductTypeDto> Handle(GetProductTypeQuery request, CancellationToken cancellationToken)
    {
        var productTypeToFind = await _repo.GetByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<ProductTypeDto>(productTypeToFind);
        }
    }
}
