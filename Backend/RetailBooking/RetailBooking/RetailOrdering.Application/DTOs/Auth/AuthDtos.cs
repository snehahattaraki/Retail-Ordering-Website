using System;

namespace RetailOrdering.Application.DTOs.Auth
{
    public record RegisterRequestDto(string Email, string Password, string FullName);
    public record LoginRequestDto(string Email, string Password);
    public record AuthResponseDto(string Token, string RefreshToken, DateTime ExpiresAt);
}
