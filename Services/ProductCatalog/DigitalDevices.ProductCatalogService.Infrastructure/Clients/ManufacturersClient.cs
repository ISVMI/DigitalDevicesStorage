using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Interfaces;
using System.Net.Http.Json;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Clients
{
    public class ManufacturersClient : IManufacturersClient
    {
        private readonly HttpClient _httpClient;

        public ManufacturersClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ManufacturerDto>> GetAllAsync(CancellationToken token)
        {
            var responce = await _httpClient.GetAsync("api/manufacturers/All", token);

            responce.EnsureSuccessStatusCode();

            return await responce.Content.ReadFromJsonAsync<IEnumerable<ManufacturerDto>>();
        }
    }
}
