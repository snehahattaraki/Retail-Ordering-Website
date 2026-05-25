using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Coupon : SoftDeleteEntity
    {
        public string Code { get; set; }
        public string? Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public int MaxUsageCount { get; set; }
        public int CurrentUsageCount { get; set; } = 0;
        public DateTime ValidFromDate { get; set; }
        public DateTime ValidUntilDate { get; set; }
        public decimal? MinimumOrderAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
