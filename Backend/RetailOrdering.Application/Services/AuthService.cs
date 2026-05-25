using RetailOrdering.Application.DTOs.Auth;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest) => Task.FromResult(new AuthResponseDto());
        public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest) => Task.FromResult(new AuthResponseDto());
        public Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshToken) => Task.FromResult(new AuthResponseDto());
        public Task ChangePasswordAsync(ChangePasswordDto changePassword) => Task.CompletedTask;
    }
}
