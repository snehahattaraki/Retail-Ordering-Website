using RetailOrdering.Application.DTOs.Brand;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class BrandService : IBrandService
    {
        public Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto) => Task.FromResult(new BrandDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<BrandDto>> GetAllAsync() => Task.FromResult<IEnumerable<BrandDto>>(new List<BrandDto>());
        public Task<BrandDto> GetByIdAsync(int id) => Task.FromResult(new BrandDto());
        public Task<BrandDto> UpdateAsync(int id, UpdateBrandDto updateBrandDto) => Task.FromResult(new BrandDto());
    }
}
