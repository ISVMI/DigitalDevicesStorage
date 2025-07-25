namespace Shared.Messages
{
    public sealed record NewProductTypeAdded(Guid ProductTypeId, List<Guid> CharacteristicsIds);
}
