using System;

namespace RetailOrdering.Application.DTOs.Loyalty
{
    public record LoyaltyResponseDto(Guid UserId, int Points);
}
