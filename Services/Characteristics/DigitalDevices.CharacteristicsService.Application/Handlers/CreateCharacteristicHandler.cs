using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicHandler : IRequestHandler<CreateCharacteristicCommand, Characteristics>
    {
        private readonly ICharacteristicsRepo _repo;

        public CreateCharacteristicHandler(ICharacteristicsRepo repo)
        {
            _repo = repo;
        }

        public async Task<Characteristics> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.CreateAsync(request.Characteristic, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}