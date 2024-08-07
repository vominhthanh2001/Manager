using Blazored.LocalStorage;
using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Manager.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Manager.Web.Provider
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private UserModel _instanse { get; set; }
        private readonly ILocalStorageService localStorageService;
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwtTokenFromLocalStorage = await localStorageService.GetItemAsync<string>("JwtToken");
            if (string.IsNullOrEmpty(jwtTokenFromLocalStorage))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(jwtTokenFromLocalStorage), "JwtAuth")));
        }

        public void MarkUserAsAuthenticated(string token)
        {
            localStorageService.SetItemAsync("JwtToken", token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "JwtAuth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOut()
        {
            await localStorageService.RemoveItemAsync("JwtToken");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split(".")[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void NotifyAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService localStorageService;
        public CustomHttpHandler(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.ToLower().Contains("login") || request.RequestUri!.AbsolutePath.ToLower().Contains("register"))
                return await base.SendAsync(request, cancellationToken);

            var token = await localStorageService.GetItemAsync<string>("JwtToken", cancellationToken = default);
            if (token is not null)
                request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {

            }

            return response;
        }
    }
}
