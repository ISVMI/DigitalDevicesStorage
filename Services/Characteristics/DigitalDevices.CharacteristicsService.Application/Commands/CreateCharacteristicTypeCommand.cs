using DigitalDevices.CharacteristicsService.Core.Models;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record CreateCharacteristicTypeCommand(CharacteristicsType CharacteristicsType) : IRequest<CharacteristicsType>;
}
