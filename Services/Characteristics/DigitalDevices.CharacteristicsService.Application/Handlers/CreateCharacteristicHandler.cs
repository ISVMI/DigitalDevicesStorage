using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class CreateCharacteristicHandler : IRequestHandler<CreateCharacteristicCommand, Guid>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public CreateCharacteristicHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCharacteristicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newCharacteristic = _mapper.Map<Characteristics>(request.Characteristic);
                await _repo.CreateAsync(newCharacteristic, cancellationToken);
                return newCharacteristic.Id;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't create new characteristic: {ex.Message}";
                Console.WriteLine(message);
                return Guid.Empty;
            }
        }
    }
}