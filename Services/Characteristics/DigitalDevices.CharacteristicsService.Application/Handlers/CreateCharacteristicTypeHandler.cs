using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicTypeHandler : IRequestHandler<CreateCharacteristicTypeCommand, CharacteristicsType>
    {
        private readonly ICharacteristicsTypeRepo _repo;

        public CreateCharacteristicTypeHandler(ICharacteristicsTypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<CharacteristicsType> Handle(CreateCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CreateAsync(request.CharacteristicsType, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}