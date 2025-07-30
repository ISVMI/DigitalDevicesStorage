using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record EditCharacteristicTypeCommand(EditCharacteristicTypeDto CharacteristicTypeToEdit) : IRequest<CharacteristicTypeDto>;
}
