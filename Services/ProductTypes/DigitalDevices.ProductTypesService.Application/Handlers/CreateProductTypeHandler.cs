
using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Core.Models;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, ProductTypes>
    {
        private readonly IProductTypesRepo _repo;

        public CreateProductTypeHandler(IProductTypesRepo repo)
        {
            _repo = repo;
        }

        public async Task<ProductTypes> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CreateAsync(request.ProductType, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
