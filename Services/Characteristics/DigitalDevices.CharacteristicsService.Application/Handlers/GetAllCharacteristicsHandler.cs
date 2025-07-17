using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsHandler : IRequestHandler<GetAllCharacteristicsQuery, IEnumerable<Characteristics>>
    {
        private readonly ICharacteristicsRepo _repo;

        public GetAllCharacteristicsHandler(ICharacteristicsRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Characteristics>> Handle(GetAllCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}
