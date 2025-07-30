using DigitalDevices.ProductCatalogService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Commands
{
    public record CreateProductCommand(CreateProductDto Product) : IRequest<Guid>;
}
