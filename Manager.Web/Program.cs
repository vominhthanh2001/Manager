using Blazored.LocalStorage;
using Manager.Shared.Contracts;
using Manager.Web;
using Manager.Web.Provider;
using Manager.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IListUserManager, ListUserManagerService>();
builder.Services.AddScoped<IUserManager, UserManagerService>();
builder.Services.AddScoped<IProductManager, ProductManagerService>();
//builder.Services.AddHttpClient("DevoloperAPI", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7215/");
//});

builder.Services.AddHttpClient("DevoloperAPI", client =>
{
    client.BaseAddress = new Uri("http://103.231.249.123/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomHttpHandler>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
