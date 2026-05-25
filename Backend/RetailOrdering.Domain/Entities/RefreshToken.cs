using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokedDate { get; set; }

        // Foreign Keys
        public int UserId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
}
