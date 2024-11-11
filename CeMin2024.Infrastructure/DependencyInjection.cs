using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CeMin2024.Application.Interfaces;
using CeMin2024.Infrastructure.Services;
using System.Data;
using System.Data.SqlClient;

namespace CeMin2024.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Validar configuración
            ValidateConfiguration(configuration);

            // Registrar servicios de la aplicación
            AddApplicationServices(services);

            // Registrar conexión a base de datos
            ConfigureDatabaseConnection(services, configuration);

            // Registrar HttpClient para Blazor WebAssembly
            ConfigureHttpClient(services);

            return services;
        }

        private static void ValidateConfiguration(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSAU");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("La cadena de conexión 'ConnectionSAU' no está configurada en appsettings.json");
            }

            // Validar que la cadena de conexión tenga los elementos necesarios
            if (!connectionString.Contains("Data Source") ||
                !connectionString.Contains("Initial Catalog") ||
                !connectionString.Contains("User ID") ||
                !connectionString.Contains("Password"))
            {
                throw new ApplicationException("La cadena de conexión 'ConnectionSAU' no tiene el formato correcto");
            }
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            // Servicios principales
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();

            // Aquí puedes agregar más servicios 
        }

        private static void ConfigureDatabaseConnection(
            IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionSAU");

            // Configurar opciones de base de datos
            services.Configure<DatabaseOptions>(options =>
            {
                options.ConnectionString = connectionString!;
                options.CommandTimeout = configuration.GetValue<int>("DatabaseOptions:CommandTimeout");
                options.MaxRetryAttempts = configuration.GetValue<int>("DatabaseOptions:MaxRetryAttempts");
                options.EnableDetailedErrors = configuration.GetValue<bool>("DatabaseOptions:EnableDetailedErrors");
            });

            // Registrar la conexión SQL con manejo de errores
            services.AddScoped<IDbConnection>(sp =>
            {
                var connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    return connection;
                }
                catch (SqlException ex)
                {
                    throw new ApplicationException(
                        $"Error al conectar con la base de datos: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(
                        "Error inesperado al configurar la conexión de base de datos", ex);
                }
            });
        }

        private static void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddHttpClient("CeMinAPI", client =>
            {
                // Configuración base del HttpClient
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))  // Tiempo de vida del handler
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });
        }
    }

    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int CommandTimeout { get; set; }
        public int MaxRetryAttempts { get; set; }
        public bool EnableDetailedErrors { get; set; }

        public DatabaseOptions()
        {
            CommandTimeout = 30;
            MaxRetryAttempts = 3;
            EnableDetailedErrors = false;
        }
    }
}