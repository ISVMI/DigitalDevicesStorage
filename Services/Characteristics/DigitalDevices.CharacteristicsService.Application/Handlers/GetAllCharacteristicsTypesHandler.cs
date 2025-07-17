using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsTypesHandler : IRequestHandler<GetAllCharacteristicTypesQuery, IEnumerable<CharacteristicsType>>
    {
        private readonly ICharacteristicsTypeRepo _repo;

        public GetAllCharacteristicsTypesHandler(ICharacteristicsTypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CharacteristicsType>> Handle(GetAllCharacteristicTypesQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync(cancellationToken);
        }
    }
}
