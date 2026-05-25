using System;

namespace RetailOrdering.Application.DTOs.Inventory
{
    public record InventoryResponseDto(Guid Id, Guid ProductId, int Quantity, DateTime LastUpdated);
    public record UpdateInventoryDto(Guid ProductId, int Quantity);
}
