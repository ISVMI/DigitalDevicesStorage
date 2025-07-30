using DigitalDevices.ProductTypesService.Application.Dtos;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Commands
{
    public record EditProductTypeCommand(EditProductTypeDto ProductTypeToEdit) : IRequest<ProductTypeDto>;
}
