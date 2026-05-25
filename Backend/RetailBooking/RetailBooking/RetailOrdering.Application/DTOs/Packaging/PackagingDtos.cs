using System;

namespace RetailOrdering.Application.DTOs.Packaging
{
    public record PackagingResponseDto(Guid Id, string Type, string? Details);
    public record CreatePackagingDto(string Type, string? Details);
    public record UpdatePackagingDto(string? Type, string? Details);
}
