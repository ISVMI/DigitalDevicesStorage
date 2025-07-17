using DigitalDevices.CharacteristicsService.Application.Commands;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class DeleteCharacteristicHandler : IRequestHandler<DeleteCharacteristicCommand, bool>
    {
        private readonly ICharacteristicsRepo _repo;

        public DeleteCharacteristicHandler(ICharacteristicsRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteCharacteristicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.DeleteAsync(request.Id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't delete characteristic: {ex.Message}";
                Console.WriteLine(message);
                return false;
            }
        }
    }
}
