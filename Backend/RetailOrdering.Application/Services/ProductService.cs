using RetailOrdering.Application.DTOs.Product;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class ProductService : IProductService
    {
        public Task<ProductDto> CreateAsync(CreateProductDto createProductDto) => Task.FromResult(new ProductDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<ProductDto>> GetAllAsync() => Task.FromResult<IEnumerable<ProductDto>>(new List<ProductDto>());
        public Task<ProductDto> GetByIdAsync(int id) => Task.FromResult(new ProductDto());
        public Task<ProductDto> UpdateAsync(int id, UpdateProductDto updateProductDto) => Task.FromResult(new ProductDto());
    }
}
