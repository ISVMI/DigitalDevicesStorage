
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Commands
{
    public record DeleteProductTypeCommand(Guid Id) : IRequest<bool>;
}
