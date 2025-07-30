using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsHandler : IRequestHandler<GetAllCharacteristicsQuery, IEnumerable<CharacteristicDto>>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public GetAllCharacteristicsHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacteristicDto>> Handle(GetAllCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            var characteristics = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CharacteristicDto>>(characteristics);
        }
    }
}
