using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Interfaces;

namespace DigitalDevices.CharacteristicsService.Application.Services
{
    public class CharacteristicsTypeProductTypesService : ICharacteristicsTypeProductTypesService
    {
        private readonly ICharacteristicsTypeProductTypesRepo _repo;
        public CharacteristicsTypeProductTypesService(ICharacteristicsTypeProductTypesRepo repo)
        {
            _repo = repo;
        }

        public async Task AddNewRelation(Guid productTypeId, List<Guid> characteristicsIds)
        {
            foreach (var characteristicId in characteristicsIds)
            {
                try
                {
                    await _repo.AddNewRelation(productTypeId, characteristicId);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    Console.WriteLine($"Couldn't add a new relation: {message}");
                }
            }
        }
    }
}
