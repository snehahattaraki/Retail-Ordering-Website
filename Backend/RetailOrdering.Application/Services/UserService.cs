using RetailOrdering.Application.DTOs.User;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class UserService : IUserService
    {
        public Task<UserDto> CreateAsync(UpdateUserDto createUserDto) => Task.FromResult(new UserDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<UserDto>> GetAllAsync() => Task.FromResult<IEnumerable<UserDto>>(new List<UserDto>());
        public Task<UserDto> GetByIdAsync(int id) => Task.FromResult(new UserDto());
        public Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto) => Task.FromResult(new UserDto());
    }
}
