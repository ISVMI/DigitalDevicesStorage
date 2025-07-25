using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, bool>
    {
        private readonly IProductTypesService _service;

        public DeleteProductTypeHandler(IProductTypesService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete manufacturer: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
