using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain;

namespace CeMin2024.Infrastructure.Services
{
    public class ConfigurationServiceClient : IConfigurationService  // Agregué esta línea
    {   // Agregué estas llaves de apertura y cierre de la clase
        private readonly HttpClient _httpClient;

        public ConfigurationServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetApplicationName()
        {
            return await _httpClient.GetFromJsonAsync<string>("api/configuration/appname");
        }
    }
}
