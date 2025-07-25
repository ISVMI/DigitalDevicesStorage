namespace DigitalDevices.CharacteristicsService.Application.Interfaces
{
    public interface ICharacteristicsTypeProductTypesService
    {
        public Task AddNewRelation(Guid productTypeId, List<Guid> characteristicsIds);
    }
}
