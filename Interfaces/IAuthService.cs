using GarageApp.API.DTOs;
using GarageApp.API.DTOs.Auth;

namespace GarageApp.API.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
        Task<CurrentUserDto?> GetCurrentUserAsync(string userId);
    }
}