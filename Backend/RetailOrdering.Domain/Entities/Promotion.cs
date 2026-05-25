using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Promotion : SoftDeleteEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int Priority { get; set; } = 0;

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
