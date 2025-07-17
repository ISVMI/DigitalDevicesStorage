using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record DeleteCharacteristicCommand(int Id) : IRequest<bool>;
}
