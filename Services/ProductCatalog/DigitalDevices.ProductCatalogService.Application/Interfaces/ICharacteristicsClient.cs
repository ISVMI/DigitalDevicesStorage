using DigitalDevices.ProductCatalogService.Application.Dtos;

namespace DigitalDevices.ProductCatalogService.Application.Interfaces
{
    public interface ICharacteristicsClient
    {
        Task<IEnumerable<CharacteristicsTypesDto>> GetCharacteristicsByProductTypeId(Guid productTypeId, CancellationToken token = default);
    }
}
