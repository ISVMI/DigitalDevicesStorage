using DigitalDevices.CharacteristicsService.Application.Dtos;
using DigitalDevices.CharacteristicsService.Application.Interfaces;
using DigitalDevices.CharacteristicsService.Application.Queries;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Handlers
{
    public class GetCharacteristicHandler : IRequestHandler<GetCharacteristicQuery, CharacteristicDto>
    {
        private readonly ICharacteristicsService _service;

        public GetCharacteristicHandler(ICharacteristicsService service)
        {
            _service = service;
        }

        public async Task<CharacteristicDto> Handle(GetCharacteristicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetByIdAsync(request.Id, cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                var message = $"Couldn't get characteristic: {ex.Message}";
                Console.WriteLine(message);
                return null;
            }
        }
    }
}
