namespace Shared.Messages
{
    public sealed record ProductTypeCreated(Guid ProductTypeId, List<Guid> CharacteristicTypesIds);
}
