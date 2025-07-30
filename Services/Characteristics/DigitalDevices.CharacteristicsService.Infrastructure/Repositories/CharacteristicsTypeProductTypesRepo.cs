using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Repositories
{
    public class CharacteristicsTypeProductTypesRepo : ICharacteristicsTypeProductTypesRepo
    {
        private readonly CharacteristicsContext _context;

        public CharacteristicsTypeProductTypesRepo(CharacteristicsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CharacteristicsType>> GetByProductTypeId(Guid productTypeId, CancellationToken token)
        {
            if (productTypeId == Guid.Empty)
            {
                throw new ArgumentException($"Product type id - {productTypeId} was not set");
            }

            var characteristics = await _context.CharacteristicsTypeProductTypes
                .Where(ctpt => ctpt.ProductTypesId == productTypeId)
                .Join(_context.CharacteristicsType,
                    ctpt => ctpt.CharacteristicsTypeId,
                    ct => ct.Id,
                    (ctpt, ct) => ct)
                .ToListAsync(token);

            if (!characteristics.Any())
            {
                throw new NotFoundException($"No characteristics found for ProductTypeId: {productTypeId}");
            }

            return characteristics;
        }
    }
}
