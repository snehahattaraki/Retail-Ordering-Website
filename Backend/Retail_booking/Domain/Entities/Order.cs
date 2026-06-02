using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Preparing,
    Delivered,
    Cancelled
}

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public int? CouponId { get; set; }
    public Coupon? Coupon { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public Payment? Payment { get; set; }
}
