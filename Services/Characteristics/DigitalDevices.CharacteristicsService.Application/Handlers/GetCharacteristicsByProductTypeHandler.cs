using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Queries;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicsByProductTypeHandler : IRequestHandler<GetCharacteristicsByProductTypeIdQuery, IEnumerable<CharacteristicDto>>
    {
        private readonly ICharacteristicsTypeProductTypesRepo _repo;

        public GetCharacteristicsByProductTypeHandler(ICharacteristicsTypeProductTypesRepo repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CharacteristicDto>> Handle(GetCharacteristicsByProductTypeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var characteristicsTypes = await _repo.GetByProductTypeId(request.ProductTypeId);

                var characteristics = characteristicsTypes.Select(ct => new CharacteristicDto
                    {
                        CharacteristicTypeName = ct.Name
                    })
                    .ToList();

                return characteristics;
            }
            catch (Exception ex)
            {
                var message = $"Couldn't get characteristics: {ex.Message}";
                Console.WriteLine(message);
                return Enumerable.Empty<CharacteristicDto>();
            }
        }
    }
}
