using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public class LoyaltyPoint : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int Points { get; set; }
}
