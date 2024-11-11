using CeMin2024.Application.Exceptions;
using CeMin2024.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Infrastructure.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetApplicationName()
        {
            var appName = _configuration["NombreAplicacion"];

            if (string.IsNullOrEmpty(appName))
            {
                throw new ConfigurationException("NombreAplicacion",
                    "El nombre de la aplicación no está configurado en appsettings.json");
            }

            return await Task.FromResult(appName);
        }
    }
}
