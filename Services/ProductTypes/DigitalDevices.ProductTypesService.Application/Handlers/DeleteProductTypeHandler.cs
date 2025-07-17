using DigitalDevices.ProductTypesService.Application.Commands;
using DigitalDevices.ProductTypesService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ProductTypesService.Application.Handlers
{
    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, bool>
    {
        private readonly IProductTypesRepo _repo;

        public DeleteProductTypeHandler(IProductTypesRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.DeleteAsync(request.Id, cancellationToken);
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
