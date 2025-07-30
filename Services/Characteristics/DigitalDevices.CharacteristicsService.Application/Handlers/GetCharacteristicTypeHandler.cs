using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicTypeHandler : IRequestHandler<GetCharacteristicTypeQuery, CharacteristicTypeDto>
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public GetCharacteristicTypeHandler(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CharacteristicTypeDto> Handle(GetCharacteristicTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var characteristicTypeToFind = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<CharacteristicTypeDto>(characteristicTypeToFind);
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return new CharacteristicTypeDto();
            }
        }
    }
}
