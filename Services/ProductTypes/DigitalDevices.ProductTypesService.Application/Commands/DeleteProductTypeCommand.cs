
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Commands
{
    public record DeleteProductTypeCommand(int Id) : IRequest<bool>;
}
