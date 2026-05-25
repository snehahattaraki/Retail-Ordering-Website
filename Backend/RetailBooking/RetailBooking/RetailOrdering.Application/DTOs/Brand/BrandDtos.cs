using System;

namespace RetailOrdering.Application.DTOs.Brand
{
    public record BrandResponseDto(Guid Id, string Name);
    public record CreateBrandDto(string Name);
    public record UpdateBrandDto(string? Name);
}
