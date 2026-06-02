using RetailOrdering.Application.DTOs.Common;
using RetailOrdering.Application.DTOs.Product;

namespace RetailOrdering.Application.Interfaces;

public interface IProductService
{
    Task<PaginationResponseDto<ProductDto>> GetProductsAsync(int page, int pageSize, string? search, string? category, string? brand);
    Task<ProductDto?> GetByIdAsync(int id);
    Task<IEnumerable<string>> GetBrandsAsync();
    Task<IEnumerable<string>> GetCategoriesAsync();
}