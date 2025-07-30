using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicHandler : IRequestHandler<GetCharacteristicQuery, CharacteristicDto>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public GetCharacteristicHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CharacteristicDto> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var characteristicToFind = await _repo.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<CharacteristicDto>(characteristicToFind);
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic: {ex.Message}";
                Console.WriteLine(message);
                return new CharacteristicDto();
            }
        }
    }
}
