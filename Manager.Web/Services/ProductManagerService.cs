using Manager.Shared.Contracts;
using Manager.Shared.Models;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace Manager.Web.Services
{
    public class ProductManagerService : IProductManager
    {
        private readonly HttpClient _client;
        public ProductManagerService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DevoloperAPI");
        }

        public async Task<bool> CreateAsync(string? name)
        {
            var resHttp = await _client.GetAsync($"/api/product/create/{name}");
            var response = await resHttp.Content.ReadFromJsonAsync<bool>();

            return response;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            var resHttp = await _client.GetAsync($"/api/product/delete/{id}");
            var response = await resHttp.Content.ReadFromJsonAsync<bool>();

            return response;
        }

        public async Task<ProductModel?> FindAsync(int? id)
        {
            var resHttp = await _client.GetAsync($"/api/product/find/{id}");
            var response = await resHttp.Content.ReadFromJsonAsync<ProductModel>();

            return response;
        }

        public async Task<ProductModel?> FindAsync(string? name)
        {
            var resHttp = await _client.GetAsync($"/api/product/find-by-name/{name}");
            var response = await resHttp.Content.ReadFromJsonAsync<ProductModel>();

            return response;
        }

        public async Task<bool> UpdateAsync(ProductModel? product)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/product/update", product);
            var response = await resHttp.Content.ReadFromJsonAsync<bool>();

            return response;
        }

        public async Task<List<ProductModel>?> GetAllAsync()
        {
            var resHttp = await _client.GetAsync($"/api/product/get-all");
            var response = await resHttp.Content.ReadFromJsonAsync<List<ProductModel>?>();

            return response;
        }
    }
}
