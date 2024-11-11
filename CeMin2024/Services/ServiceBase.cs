using System.Net.Http.Json;

namespace CeMin2024.Client.Services
{
    public abstract class ServiceBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        protected ServiceBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = httpClient.BaseAddress?.ToString() ?? "";
        }

        protected async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(url);
            }
            catch
            {
                return default;
            }
        }

        protected async Task<bool> PostAsync<T>(string url, T data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, data);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}