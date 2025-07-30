using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicTypeHandler : IRequestHandler<CreateCharacteristicTypeCommand, Guid>
    {
        private readonly ICharacteristicsTypeRepo _repo;
        private readonly IMapper _mapper;

        public CreateCharacteristicTypeHandler(ICharacteristicsTypeRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var characteristicType = _mapper.Map<CharacteristicsType>(request.CharacteristicsType);
                await _repo.CreateAsync(characteristicType, cancellationToken);
                return characteristicType.Id;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return Guid.Empty;
            }
        }
    }
}