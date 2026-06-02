using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Status { get; set; } = null!;
}
