using CeMin2024.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using CeMin2024.Client.Services;
using CeMin2024.Application.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Servicios de autenticación
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// Registrar servicios del cliente
builder.Services.AddScoped<IAuthService, AuthServiceClient>();
builder.Services.AddScoped<IRoleService, RoleServiceClient>();
builder.Services.AddScoped<IConfigurationService, ConfigurationServiceClient>();

await builder.Build().RunAsync();