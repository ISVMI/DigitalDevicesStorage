using DigitalDevices.ManufacturersService.Application.Commands;
using DigitalDevices.ManufacturersService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.ManufacturersService.Application.Handlers
{
    internal class DeleteManufacturerHandler : IRequestHandler<DeleteManufacturerCommand, bool>
    {
        private readonly IManufacturersRepo _repo;

        public DeleteManufacturerHandler(IManufacturersRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
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
