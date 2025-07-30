
using MediatR;

namespace DigitalDevices.ProductCatalogService.Application.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;
}
