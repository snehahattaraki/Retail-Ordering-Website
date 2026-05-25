using System;

namespace RetailOrdering.Application.DTOs.Coupon
{
    public record CouponResponseDto(Guid Id, string Code, decimal DiscountPercent, DateTime Expiry);
    public record CreateCouponDto(string Code, decimal DiscountPercent, DateTime Expiry);
}
