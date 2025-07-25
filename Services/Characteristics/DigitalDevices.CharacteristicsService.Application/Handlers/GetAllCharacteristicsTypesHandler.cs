using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsTypesHandler : IRequestHandler<GetAllCharacteristicTypesQuery, IEnumerable<CharacteristicTypeDto>>
    {
        private readonly ICharacteristicsTypeService _service;

        public GetAllCharacteristicsTypesHandler(ICharacteristicsTypeService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CharacteristicTypeDto>> Handle(GetAllCharacteristicTypesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
