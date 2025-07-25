using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record CreateCharacteristicCommand(CreateCharacteristicDto Characteristic) : IRequest<Guid>{ }
}
