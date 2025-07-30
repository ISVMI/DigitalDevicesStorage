using DigitalDevices.ProductCatalogService.Application.Dtos;
using DigitalDevices.ProductCatalogService.Application.Interfaces;
using System.Net.Http.Json;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Clients
{
    public class ProductTypesClient : IProductTypesClient
    {
        private readonly HttpClient _httpclient;

        public ProductTypesClient(HttpClient httpClient)
        {
            _httpclient = httpClient;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllAsync(CancellationToken token)
        {
            var responce = await _httpclient.GetAsync("api/producttypes/All", token);
            
            responce.EnsureSuccessStatusCode();

            return await responce.Content.ReadFromJsonAsync<IEnumerable<ProductTypeDto>>();
        }
    }
}
