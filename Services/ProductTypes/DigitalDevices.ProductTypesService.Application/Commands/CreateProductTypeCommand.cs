using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Commands
{
    public record CreateProductTypeCommand (ProductTypes ProductType) : IRequest<ProductTypes>;
}
