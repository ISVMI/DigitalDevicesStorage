using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record CreateCharacteristicCommand(Characteristics Characteristic) : IRequest<Characteristics>{ }
}
