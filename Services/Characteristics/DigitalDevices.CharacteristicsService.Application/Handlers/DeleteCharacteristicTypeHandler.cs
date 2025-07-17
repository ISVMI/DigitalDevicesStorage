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
            try
            {
                var result = await _repo.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
