using System;
using System.Collections.Generic;

namespace RetailOrdering.Application.DTOs.Order
{
    public record OrderItemDto(Guid ProductId, int Quantity, decimal UnitPrice);
    public record OrderResponseDto(Guid Id, Guid UserId, IEnumerable<OrderItemDto> Items, decimal Total, DateTime PlacedAt, string Status);
    public record CreateOrderDto(Guid UserId, IEnumerable<OrderItemDto> Items, string? CouponCode);
}
