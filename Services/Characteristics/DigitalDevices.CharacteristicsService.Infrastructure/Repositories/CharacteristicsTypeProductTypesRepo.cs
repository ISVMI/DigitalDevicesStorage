using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Repositories
{
    public class CharacteristicsTypeProductTypesRepo : ICharacteristicsTypeProductTypesRepo
    {
        private readonly CharacteristicsContext _context;

        public CharacteristicsTypeProductTypesRepo(CharacteristicsContext context)
        {
            _context = context;
        }

        public async Task AddNewRelation(Guid productTypesId, Guid characteristicsTypeId)
        {

            if (characteristicsTypeId == Guid.Empty || productTypesId == Guid.Empty)
            {
                throw new Exception($"One of the given ids: product type id - {productTypesId}, characteristic type id - {characteristicsTypeId} was not set");
            }

            var newRelation = new CharacteristicsTypeProductTypes
            {
                CharacteristicsTypeId = characteristicsTypeId,
                ProductTypesId = productTypesId
            };

            await _context.CharacteristicsTypeProductTypes.AddAsync(newRelation);
            await _context.SaveChangesAsync();
        }
    }
}
