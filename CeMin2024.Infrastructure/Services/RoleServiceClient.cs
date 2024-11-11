using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;

namespace CeMin2024.Infrastructure.Services
{
    public class RoleServiceClient : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<RoleModel>>("api/role");
        }

        // Implementación del método faltante
        public async Task<bool> ExisteRolAsync(string nombre)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/role/exists/{nombre}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Implementación del método faltante
        public async Task<RoleModel> GetRoleByNombreAsync(string nombre)
        {
            return await _httpClient.GetFromJsonAsync<RoleModel>($"api/role/byname/{nombre}");
        }
    }
}