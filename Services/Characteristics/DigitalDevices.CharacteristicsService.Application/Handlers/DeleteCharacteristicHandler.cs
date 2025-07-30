using AutoMapper;
using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class DeleteCharacteristicHandler : IRequestHandler<DeleteCharacteristicCommand, bool>
    {
        private readonly ICharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public DeleteCharacteristicHandler(ICharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
