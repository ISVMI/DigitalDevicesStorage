using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class DeleteCharacteristicHandler : IRequestHandler<DeleteCharacteristicCommand, bool>
    {
        private readonly ICharacteristicsService _service;

        public DeleteCharacteristicHandler(ICharacteristicsService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete characteristic: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
