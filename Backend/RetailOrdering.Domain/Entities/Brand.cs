using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Brand : SoftDeleteEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
