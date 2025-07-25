using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record CreateCharacteristicTypeCommand(CreateCharacteristicTypeDto CharacteristicsType) : IRequest<Guid>;
}
