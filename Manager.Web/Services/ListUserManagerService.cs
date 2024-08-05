using Manager.Shared.Contracts;
using Manager.Shared.Models;
using System.Net.Http.Json;

namespace Manager.Web.Services
{
    public class ListUserManagerService : IListUserManager
    {
        private readonly HttpClient _client;
        public ListUserManagerService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DevoloperAPI");
        }

        public async Task<ListUserResponseModel> AddAsync(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/list-user-manager/add", user);
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }

        public async Task<ListUserResponseModel> AddAsync(List<UserModel?>? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/list-user-manager/add-list", user);
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }

        public async Task<ListUserResponseModel> DeleteAsync(int? id)
        {
            var resHttp = await _client.GetAsync($"/api/list-user-manager/delete/{id}");
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }

        public async Task<ListUserResponseModel> DeleteAsync(List<int?>? ids)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/list-user-manager/delete-list", ids);
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }

        public async Task<UserModel?> FindAsync(int? id)
        {
            var resHttp = await _client.GetAsync($"/api/list-user-manager/find/{id}");
            var response = await resHttp.Content.ReadFromJsonAsync<UserModel>();

            return response;
        }

        public async Task<List<UserModel?>?> GetAllAsync()
        {
            var resHttp = await _client.GetAsync($"/api/list-user-manager/get-all");
            var response = await resHttp.Content.ReadFromJsonAsync<List<UserModel?>?>();

            return response;
        }

        public async Task<ListUserResponseModel> UpdateAsync(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/list-user-manager/update", user);
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }

        public async Task<ListUserResponseModel> UpdateAsync(List<UserModel?> users)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/list-user-manager/update-list", users);
            var response = await resHttp.Content.ReadFromJsonAsync<ListUserResponseModel>();

            return response;
        }
    }
}
