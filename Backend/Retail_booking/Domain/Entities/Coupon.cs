using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = null!;
    public decimal Discount { get; set; }
    public ICollection<Order>? Orders { get; set; }
}
