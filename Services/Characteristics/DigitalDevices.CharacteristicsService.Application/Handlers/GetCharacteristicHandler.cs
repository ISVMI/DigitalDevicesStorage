using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicHandler : IRequestHandler<GetCharacteristicQuery, Characteristics>
    {
        private readonly ICharacteristicsRepo _repo;

        public GetCharacteristicHandler(ICharacteristicsRepo repo)
        {
            _repo = repo;
        }

        public async Task<Characteristics> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
