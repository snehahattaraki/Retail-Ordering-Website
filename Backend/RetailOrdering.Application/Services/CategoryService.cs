using RetailOrdering.Application.DTOs.Category;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto) => Task.FromResult(new CategoryDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<CategoryDto>> GetAllAsync() => Task.FromResult<IEnumerable<CategoryDto>>(new List<CategoryDto>());
        public Task<CategoryDto> GetByIdAsync(int id) => Task.FromResult(new CategoryDto());
        public Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto) => Task.FromResult(new CategoryDto());
    }
}
