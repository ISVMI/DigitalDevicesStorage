using DigitalDevices.ProductTypesService.Application.Dtos;
using MediatR;
using Shared.Responses;

namespace DigitalDevices.ProductTypesService.Application.Queries
{
    public record GetAllProductTypesPagedQuery(int Page = 1, int PageSize = 20) : IRequest<PagedResponse<ProductTypeDto>>;
}
