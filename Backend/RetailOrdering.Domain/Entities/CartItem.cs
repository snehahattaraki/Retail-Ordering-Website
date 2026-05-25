using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        // Foreign Keys
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
