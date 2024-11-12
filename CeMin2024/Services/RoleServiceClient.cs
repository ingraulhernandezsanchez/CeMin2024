using System.Net.Http.Json;
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CeMin2024.Client.Services
{
    public class RoleServiceClient : IRoleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RoleServiceClient> _logger;

        public RoleServiceClient(HttpClient httpClient, ILogger<RoleServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<RoleModel>> GetRolesAsync()
        {
            try
            {
                var roles = await _httpClient.GetFromJsonAsync<List<RoleModel>>("api/role");
                return roles ?? new List<RoleModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles");
                throw;
            }
        }

        public async Task<RoleModel?> GetRoleByNombreAsync(string nombre)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RoleModel>($"api/role/{nombre}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol {Nombre}", nombre);
                throw;
            }
        }

        public async Task<bool> ExisteRolAsync(string nombre)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/role/exists/{nombre}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<bool>();
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia del rol {Nombre}", nombre);
                throw;
            }
        }
    }
}
