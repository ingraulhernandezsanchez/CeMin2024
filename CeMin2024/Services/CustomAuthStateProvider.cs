// CeMin2024.Client/Services/CustomAuthStateProvider.cs
using CeMin2024.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CeMin2024.Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public CustomAuthStateProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var isAuthenticated = await _authService.IsAuthenticatedAsync();

                if (isAuthenticated)
                {
                    // Obtener el usuario actual y sus claims
                    // Esto deberá adaptarse a tu lógica de autenticación
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, "usuario"),
                        new Claim(ClaimTypes.Role, "role")
                    };

                    var identity = new ClaimsIdentity(claims, "custom-auth");
                    var principal = new ClaimsPrincipal(identity);

                    return new AuthenticationState(principal);
                }
            }
            catch
            {
                // Log error si es necesario
            }

            // Usuario no autenticado
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}