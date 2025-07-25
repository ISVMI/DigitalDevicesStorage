using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using DigitalDevices.ProductTypesService.Application.Queries;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductTypeDto>>
    {
            private readonly IProductTypesService _service;

            public GetAllProductTypesHandler(IProductTypesService service)
            {
                _service = service;
            }

            public async Task<IEnumerable<ProductTypeDto>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
            {
                return await _service.GetAllAsync(cancellationToken);
            }
    }
}
