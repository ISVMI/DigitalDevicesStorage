using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class DeleteCharacteristicTypeHandler : IRequestHandler<DeleteCharacteristicTypeCommand, bool>
    {
        private readonly ICharacteristicsTypeService _service;

        public DeleteCharacteristicTypeHandler(ICharacteristicsTypeService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
