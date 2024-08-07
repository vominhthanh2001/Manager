using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Manager.Web.Services
{
    public class UserManagerService : IUserManager
    {
        private readonly HttpClient _client;
        public UserManagerService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("DevoloperAPI");
        }

        public async Task<UserModel> ForgotPassword(string username)
        {
            var resHttp = await _client.GetAsync($"/api/user-manager/forgot-password/{username}");
            var response = await resHttp.Content.ReadFromJsonAsync<UserModel>();

            return response;
        }

        public async Task<UserResponseModel> Login(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/user-manager/login", user);
            var response = await resHttp.Content.ReadFromJsonAsync<UserResponseModel>();

            return response;
        }

        public async Task<UserResponseModel> Logout(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/user-manager/logout", user);
            var response = await resHttp.Content.ReadFromJsonAsync<UserResponseModel>();

            return response;
        }

        public async Task<UserResponseModel> RefreshToken(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/user-manager/refresh-token", user);
            var response = await resHttp.Content.ReadFromJsonAsync<UserResponseModel>();

            return response;
        }

        public async Task<UserResponseModel> Register(UserModel? user)
        {
            var resHttp = await _client.PostAsJsonAsync($"/api/user-manager/register", user);
            var response = await resHttp.Content.ReadFromJsonAsync<UserResponseModel>();

            return response;
        }
    }
}
