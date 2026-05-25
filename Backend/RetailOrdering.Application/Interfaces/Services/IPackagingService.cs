using RetailOrdering.Application.DTOs.Packaging;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IPackagingService
    {
        Task<IEnumerable<PackagingDto>> GetAllAsync();
        Task<PackagingDto> GetByIdAsync(int id);
        Task<PackagingDto> CreateAsync(CreatePackagingDto createPackagingDto);
        Task<PackagingDto> UpdateAsync(int id, UpdatePackagingDto updatePackagingDto);
        Task DeleteAsync(int id);
    }
}
