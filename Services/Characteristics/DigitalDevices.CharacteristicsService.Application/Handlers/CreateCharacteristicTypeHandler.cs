using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicTypeHandler : IRequestHandler<CreateCharacteristicTypeCommand, Guid>
    {
        private readonly ICharacteristicsTypeService _service;

        public CreateCharacteristicTypeHandler(ICharacteristicsTypeService service)
        {
            _service = service;
        }

        public async Task<Guid> Handle(CreateCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.CreateAsync(request.CharacteristicsType, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return Guid.Empty;
            }
        }
    }
}