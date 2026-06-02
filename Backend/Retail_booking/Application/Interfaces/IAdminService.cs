using RetailOrdering.Application.DTOs.Admin;
using RetailOrdering.Application.DTOs.User;

namespace RetailOrdering.Application.Interfaces;

public interface IAdminService
{
    Task<AdminStatsDto> GetStatsAsync();
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> CreateUserAsync(UserDto dto);
    Task<bool> UpdateUserAsync(int id, UserDto dto);
    Task<bool> DeleteUserAsync(int id);
}