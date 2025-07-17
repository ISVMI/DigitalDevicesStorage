using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DigitalDevices.CharacteristicsService.Application.Commands
{
    public record DeleteCharacteristicTypeCommand(int Id) : IRequest<bool>;
}
