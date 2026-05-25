using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; } = false;
        public int DisplayOrder { get; set; } = 0;
        public string? AlternativeText { get; set; }

        // Foreign Keys
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; }
    }
}
