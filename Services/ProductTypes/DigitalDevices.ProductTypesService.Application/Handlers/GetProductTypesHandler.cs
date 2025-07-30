using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Queries;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetProductTypesHandler : IRequestHandler<GetAllProductTypesPagedQuery, PagedResponse<ProductTypeDto>>
    {
        private readonly IProductTypesRepo _repo;
        private readonly IMapper _mapper;

        public GetProductTypesHandler(IProductTypesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ProductTypeDto>> Handle(GetAllProductTypesPagedQuery request, CancellationToken token)
        {
            var (items, totalCount) = await _repo.GetPagedAsync(request.Page, request.PageSize, token);

            return new PagedResponse<ProductTypeDto>(_mapper.Map<IEnumerable<ProductTypeDto>>(items), totalCount,
                request.Page, request.PageSize);
        }
    }
}
