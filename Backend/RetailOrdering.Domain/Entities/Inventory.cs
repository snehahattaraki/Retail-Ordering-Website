using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Inventory : AuditableEntity
    {
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public string WarehouseLocation { get; set; }
        public DateTime? LastStockedDate { get; set; }

        // Foreign Keys
        public int ProductId { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; }
    }
}
