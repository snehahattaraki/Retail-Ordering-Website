using System;

namespace RetailOrdering.Application.DTOs.User
{
    public record UserResponseDto(Guid Id, string Email, string FullName, string Role);
    public record CreateUserDto(string Email, string Password, string FullName, string Role);
    public record UpdateUserDto(string? FullName, string? Role);
}
