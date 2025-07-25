using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetAllCharacteristicsHandler : IRequestHandler<GetAllCharacteristicsQuery, IEnumerable<CharacteristicDto>>
    {
        private readonly ICharacteristicsService _service;

        public GetAllCharacteristicsHandler(ICharacteristicsService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<CharacteristicDto>> Handle(GetAllCharacteristicsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(cancellationToken);
        }
    }
}
