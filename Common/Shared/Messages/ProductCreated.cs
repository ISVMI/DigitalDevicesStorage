using Shared.Dtos;

namespace Shared.Messages
{
    public sealed record ProductCreated(Guid ProductId, List<CharacteristicMessageDto> Characteristics);
}
