using System.Net.Http.Json;
using CeMin2024.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CeMin2024.Client.Services
{
    public class ConfigurationServiceClient : IConfigurationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ConfigurationServiceClient> _logger;

        public ConfigurationServiceClient(HttpClient httpClient, ILogger<ConfigurationServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetAppName()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<string>("api/configuration/appname");
                return response ?? "CEMIN";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el nombre de la aplicación");
                throw;
            }
        }
    }
}
