using DigitalDevices.ProductCatalogService.Application.Dtos;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ProductCatalogService.Application.Queries
{
    public record GetAllProductsPagedQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResponse<ProductsByTypeDto>>;
}
