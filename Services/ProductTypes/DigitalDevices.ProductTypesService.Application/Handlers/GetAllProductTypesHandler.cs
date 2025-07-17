using DigitalDevices.ProductTypesService.Application.Queries;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductTypes>>
    {
            private readonly IProductTypesRepo _repo;

            public GetAllProductTypesHandler(IProductTypesRepo repo)
            {
                _repo = repo;
            }

            public async Task<IEnumerable<ProductTypes>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetAllAsync(cancellationToken);
            }
    }
}
