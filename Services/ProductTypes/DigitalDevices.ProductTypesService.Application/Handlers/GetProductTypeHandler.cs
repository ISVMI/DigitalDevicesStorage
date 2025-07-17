
using DigitalDevices.ProductTypesService.Application.Queries;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class GetProductTypeHandler : IRequestHandler<GetProductTypeQuery, ProductTypes>

    {
    private readonly IProductTypesRepo _repo;

    public GetProductTypeHandler(IProductTypesRepo repo)
    {
        _repo = repo;
    }

    public async Task<ProductTypes> Handle(GetProductTypeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repo.GetByIdAsync(request.Id, cancellationToken);
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
