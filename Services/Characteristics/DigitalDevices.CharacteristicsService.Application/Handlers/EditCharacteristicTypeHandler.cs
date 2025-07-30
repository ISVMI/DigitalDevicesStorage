using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class EditCharacteristicTypeHandler : IRequestHandler<EditCharacteristicTypeCommand, CharacteristicTypeDto>
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public EditCharacteristicTypeHandler(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CharacteristicTypeDto> Handle(EditCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            var characteristicTypeToEdit = _mapper.Map<CharacteristicsType>(request.CharacteristicTypeToEdit);
            await _repo.UpdateAsync(characteristicTypeToEdit, cancellationToken);
            return _mapper.Map<CharacteristicTypeDto>(characteristicTypeToEdit);
        }
    }
}
