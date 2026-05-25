using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Product : SoftDeleteEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int Rating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        // Foreign Keys
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int PackagingId { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Packaging Packaging { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    }
}
