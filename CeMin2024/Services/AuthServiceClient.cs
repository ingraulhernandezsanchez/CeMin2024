// CeMin2024.Client/Services/AuthServiceClient.cs
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace CeMin2024.Client.Services
{
    public class AuthServiceClient : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthServiceClient(
            HttpClient httpClient,
            AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/auth/is-authenticated");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("api/auth/logout", null);
            ((CustomAuthStateProvider)_authStateProvider).NotifyAuthenticationStateChanged();
        }
    }
}