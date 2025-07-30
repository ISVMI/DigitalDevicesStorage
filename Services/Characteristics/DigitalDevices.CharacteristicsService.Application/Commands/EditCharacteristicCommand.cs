using DigitalDevices.CharacteristicsService.Application.Dtos;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record EditCharacteristicCommand(EditCharacteristicDto CharacteristicToEdit) : IRequest<CharacteristicDto>;
}
