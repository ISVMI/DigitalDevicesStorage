using DigitalDevices.ProductCatalogService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Queries
{
    public record GetProductQuery(Guid Id) : IRequest<ProductsByTypeDto>;
}
