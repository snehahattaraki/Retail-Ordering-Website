using System;

namespace RetailOrdering.Application.DTOs.Product
{
    public record ProductResponseDto(Guid Id, string Name, string Description, decimal Price, int Stock, string Category, string Brand);
    public record CreateProductDto(string Name, string Description, decimal Price, Guid CategoryId, Guid BrandId, int Stock);
    public record UpdateProductDto(string? Name, string? Description, decimal? Price, Guid? CategoryId, Guid? BrandId, int? Stock);
    public record ProductSearchDto(string? Query, Guid? CategoryId, Guid? BrandId, int Page = 1, int PageSize = 20);
}
