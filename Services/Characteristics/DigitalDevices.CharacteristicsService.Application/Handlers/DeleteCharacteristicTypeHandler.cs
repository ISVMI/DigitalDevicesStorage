using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class DeleteCharacteristicTypeHandler : IRequestHandler<DeleteCharacteristicTypeCommand, bool>
    {
        private readonly ICharacteristicsTypeRepo _repo;

        public DeleteCharacteristicTypeHandler(ICharacteristicsTypeRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteCharacteristicTypeCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
