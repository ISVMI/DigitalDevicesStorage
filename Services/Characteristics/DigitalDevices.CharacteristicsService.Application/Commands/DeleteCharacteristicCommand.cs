using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record DeleteCharacteristicCommand(Guid Id) : IRequest<bool>;
}
