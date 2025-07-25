using DigitalDevices.ProductTypesService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Commands
{
    public record CreateProductTypeCommand (CreateProductTypeDto ProductType) : IRequest<Guid>;
}
