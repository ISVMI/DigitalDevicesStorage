using DigitalDevices.ProductCatalogService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Queries
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductsByTypeDto>>;
}
