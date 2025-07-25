using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, Guid>
    {
        private readonly IProductTypesService _service;

        public CreateProductTypeHandler(IProductTypesService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CreateAsync(request.ProductType, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return Guid.Empty;
            }
        }
    }
}
