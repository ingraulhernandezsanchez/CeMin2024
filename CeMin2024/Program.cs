using System.Net.Http;
using CeMin2024.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configura el HttpClient para apuntar a la API en el puerto 9000
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:44388")
});

await builder.Build().RunAsync();
