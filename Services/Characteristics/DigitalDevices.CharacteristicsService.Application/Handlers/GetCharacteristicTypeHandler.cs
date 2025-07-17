using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicTypeHandler : IRequestHandler<GetCharacteristicTypesQuery, CharacteristicsType>
    {
        private readonly ICharacteristicsTypeRepo _repo;

        public GetCharacteristicTypeHandler(ICharacteristicsTypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<CharacteristicsType> Handle(GetCharacteristicTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
