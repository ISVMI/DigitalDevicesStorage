using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record DeleteCharacteristicTypeCommand(Guid Id) : IRequest<bool>;
}
