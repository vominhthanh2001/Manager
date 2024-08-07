using Manager.Api.Data;
using Manager.Api.Services;
using Manager.Shared.Contracts;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connect Database
string? connectString = builder.Configuration.GetConnectionString("Devoloper");
if (string.IsNullOrWhiteSpace(connectString))
    throw new ArgumentNullException(nameof(connectString));
builder.Services.AddDbContext<ServerDbContext>(o => o.UseSqlServer(connectString));
/**********************************************/

//Đăng ký services
builder.Services.AddScoped<IListUserManager, ListUserManagerService>();
builder.Services.AddScoped<IProductManager, ProductService>();
builder.Services.AddScoped<IUserManager, UserManagerService>();
builder.Services.AddScoped<IAntiHacker, AntiHackerService>();
builder.Services.AddScoped<TokenService>();
/**********************************************/

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    if (key.Length == 0)
    {
        throw new ArgumentException("JWT key must not be empty.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:7198")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithHeaders(HeaderNames.ContentType);
});
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();