using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
        public string? CouponCode { get; set; }
        public decimal? DiscountAmount { get; set; }

        // Foreign Keys
        public int UserId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
