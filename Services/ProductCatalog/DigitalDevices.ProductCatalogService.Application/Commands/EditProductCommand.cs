using DigitalDevices.ProductCatalogService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Commands
{
    public record EditProductCommand(EditProductDto ProductToEdit) : IRequest<ProductsByTypeDto>;
}
