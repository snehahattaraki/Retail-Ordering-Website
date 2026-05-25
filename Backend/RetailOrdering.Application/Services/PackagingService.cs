using RetailOrdering.Application.DTOs.Packaging;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class PackagingService : IPackagingService
    {
        public Task<PackagingDto> CreateAsync(CreatePackagingDto createPackagingDto) => Task.FromResult(new PackagingDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<PackagingDto>> GetAllAsync() => Task.FromResult<IEnumerable<PackagingDto>>(new List<PackagingDto>());
        public Task<PackagingDto> GetByIdAsync(int id) => Task.FromResult(new PackagingDto());
        public Task<PackagingDto> UpdateAsync(int id, UpdatePackagingDto updatePackagingDto) => Task.FromResult(new PackagingDto());
    }
}
