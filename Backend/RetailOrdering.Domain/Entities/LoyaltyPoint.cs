using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class LoyaltyPoint : AuditableEntity
    {
        public int Points { get; set; }
        public string Description { get; set; }
        public bool IsRedeemed { get; set; } = false;
        public DateTime? RedeemedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Foreign Keys
        public int UserId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
}
