using DigitalDevices.ProductTypesService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Queries
{
    public record GetProductTypeQuery(Guid Id) : IRequest<ProductTypeDto>;
}
