using RetailOrdering.Application.DTOs.Auth;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshToken);
        Task ChangePasswordAsync(ChangePasswordDto changePassword);
    }
}
