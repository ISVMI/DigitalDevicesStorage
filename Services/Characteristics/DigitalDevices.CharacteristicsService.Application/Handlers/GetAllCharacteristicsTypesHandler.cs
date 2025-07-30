using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsTypesHandler : IRequestHandler<GetAllCharacteristicTypesQuery, IEnumerable<CharacteristicTypeDto>>
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public GetAllCharacteristicsTypesHandler(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicTypeDto>> Handle(GetAllCharacteristicTypesQuery request, CancellationToken cancellationToken)
        {
            var characteristicsTypes = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CharacteristicTypeDto>>(characteristicsTypes);
        }
    }
}
