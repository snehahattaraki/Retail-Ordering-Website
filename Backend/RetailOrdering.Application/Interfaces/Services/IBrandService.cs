using RetailOrdering.Application.DTOs.Brand;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllAsync();
        Task<BrandDto> GetByIdAsync(int id);
        Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto);
        Task<BrandDto> UpdateAsync(int id, UpdateBrandDto updateBrandDto);
        Task DeleteAsync(int id);
    }
}
