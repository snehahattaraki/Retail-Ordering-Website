using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Packaging : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; } = "kg";
        public decimal? Dimensions { get; set; }
        public string? DimensionUnit { get; set; } = "cm";
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
