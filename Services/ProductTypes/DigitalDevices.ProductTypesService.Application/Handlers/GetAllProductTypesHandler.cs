using AutoMapper;
using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Queries;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductTypeDto>>
    {
        private readonly IProductTypesRepo _repo;
        private readonly IMapper _mapper;

        public GetAllProductTypesHandler(IProductTypesRepo repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ProductTypeDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
            {
            var productTypes = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductTypeDto>>(productTypes);
        }
    }
}
