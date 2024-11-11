using CeMin2024.Application.Common.Middleware;
using CeMin2024.Infrastructure;
using CeMin2024.Server.Options;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Añadir servicios al contenedor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configurar CORS
builder.Services.Configure<CorsOptions>(
    builder.Configuration.GetSection("Cors"));

builder.Services.AddCors(options =>
{
    var corsOptions = builder.Configuration
        .GetSection("Cors")
        .Get<CorsOptions>();

    options.AddPolicy("CeMinPolicy", policy =>
    {
        var origins = corsOptions?.AllowedOrigins ?? Array.Empty<string>();

        if (builder.Environment.IsDevelopment())
        {
            policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        }
        else
        {
            policy.WithOrigins(origins);
        }

        policy.AllowAnyMethod()
              .AllowAnyHeader();

        if (corsOptions?.AllowCredentials ?? false)
        {
            policy.AllowCredentials();
        }

        if (corsOptions?.ExposedHeaders?.Length > 0)
        {
            policy.WithExposedHeaders(corsOptions.ExposedHeaders);
        }
    });
});

// Agregar servicios de infraestructura
builder.Services.AddInfrastructure(builder.Configuration);

// Configurar compresión de respuesta
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

// Aplicar CORS - debe ir después de UseRouting pero antes de UseAuthentication
app.UseCors("CeMinPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Middleware personalizado para manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();