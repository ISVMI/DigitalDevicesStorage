using DigitalDevices.ProductTypesService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Queries
{
    public record GetAllProductTypesQuery : IRequest<IEnumerable<ProductTypeDto>>;
}
