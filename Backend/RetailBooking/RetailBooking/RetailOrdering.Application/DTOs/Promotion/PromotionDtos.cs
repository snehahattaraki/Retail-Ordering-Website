using System;

namespace RetailOrdering.Application.DTOs.Promotion
{
    public record PromotionResponseDto(Guid Id, string Name, decimal DiscountPercent, DateTime Start, DateTime End);
    public record CreatePromotionDto(string Name, decimal DiscountPercent, DateTime Start, DateTime End);
}
