using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicTypeHandler : IRequestHandler<GetCharacteristicTypesQuery, CharacteristicTypeDto>
    {
        private readonly ICharacteristicsTypeService _service;

        public GetCharacteristicTypeHandler(ICharacteristicsTypeService service)
        {
            _service = service;
        }

        public async Task<CharacteristicTypeDto> Handle(GetCharacteristicTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetByIdAsync(request.Id, cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic type: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
