using System;
using System.Collections.Generic;

namespace RetailOrdering.Application.DTOs.Cart
{
    public record CartItemDto(Guid ProductId, int Quantity, decimal UnitPrice);
    public record CartResponseDto(Guid Id, Guid UserId, IEnumerable<CartItemDto> Items, decimal SubTotal, decimal Tax, decimal Total);
    public record AddCartItemDto(Guid ProductId, int Quantity);
    public record UpdateCartItemDto(Guid ProductId, int Quantity);
}
