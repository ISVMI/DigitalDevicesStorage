namespace DigitalDevices.CharacteristicsService.Core.Interfaces
{
    public interface ICharacteristicsTypeProductTypesRepo
    {
        Task AddNewRelation(Guid characteristicsTypeId, Guid productTypesId);
    }
}
