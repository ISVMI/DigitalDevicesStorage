using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class EditCharacteristicHandler : IRequestHandler<GetCharacteristicQuery, CharacteristicDto>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public EditCharacteristicHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CharacteristicDto> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            var characteristicToEdit = await _repo.GetByIdAsync(request.Id, cancellationToken);
            await _repo.UpdateAsync(characteristicToEdit, cancellationToken);
            return _mapper.Map<CharacteristicDto>(characteristicToEdit);
        }
    }
}
