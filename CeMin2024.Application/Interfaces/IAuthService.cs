using CeMin2024.Domain.Models;

namespace CeMin2024.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel loginModel);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
    }
}