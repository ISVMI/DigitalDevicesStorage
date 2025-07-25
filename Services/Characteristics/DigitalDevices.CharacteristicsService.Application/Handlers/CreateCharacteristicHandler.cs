using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicHandler : IRequestHandler<CreateCharacteristicCommand, Guid>
    {
        private readonly ICharacteristicsService _service;

        public CreateCharacteristicHandler(ICharacteristicsService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CreateAsync(request.Characteristic, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic: {ex.Message}";
                Console.WriteLine(message);
                return Guid.Empty;
            }
        }
    }
}