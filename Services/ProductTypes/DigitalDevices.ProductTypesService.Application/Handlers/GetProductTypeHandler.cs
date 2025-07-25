using DigitalDevices.ProductTypesService.Application.Dtos;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using DigitalDevices.ProductTypesService.Application.Queries;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetProductTypeHandler : IRequestHandler<GetProductTypeQuery, ProductTypeDto>

    {
    private readonly IProductTypesService _service;

    public GetProductTypeHandler(IProductTypesService service)
    {
        _service = service;
    }

    public async Task<ProductTypeDto> Handle(GetProductTypeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetByIdAsync(request.Id, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            var message = $"Couldn't get manufacturer: {ex.Message}";
            Console.WriteLine(message);
            return null;
        }
    }
    }
}
