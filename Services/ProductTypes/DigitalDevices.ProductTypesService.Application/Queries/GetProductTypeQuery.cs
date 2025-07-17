
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Queries
{
    public record GetProductTypeQuery(int Id) : IRequest<ProductTypes>;
}
