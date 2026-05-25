using System;

namespace RetailOrdering.Application.DTOs.Category
{
    public record CategoryResponseDto(Guid Id, string Name, string? Description);
    public record CreateCategoryDto(string Name, string? Description);
    public record UpdateCategoryDto(string? Name, string? Description);
}
