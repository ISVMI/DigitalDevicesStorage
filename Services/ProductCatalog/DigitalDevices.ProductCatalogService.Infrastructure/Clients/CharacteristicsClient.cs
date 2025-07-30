using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Interfaces;
using System.Net.Http.Json;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Clients
{
    public class CharacteristicsClient : ICharacteristicsClient
    {
        private readonly HttpClient _httpClient;

        public CharacteristicsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CharacteristicsTypesDto>> GetCharacteristicsByProductTypeId(Guid productTypeId, CancellationToken token)
        {
            var responce = await _httpClient.GetAsync($"api/characteristics/GetCharacteristicsByProductTypeId/{productTypeId}", token);

            responce.EnsureSuccessStatusCode();

            return await responce.Content.ReadFromJsonAsync<IEnumerable<CharacteristicsTypesDto>>();
        }
    }
}
