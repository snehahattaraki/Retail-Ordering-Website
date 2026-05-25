using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? CardLastFour { get; set; }
        public string? GatewayResponse { get; set; }
        public bool IsRefunded { get; set; } = false;
        public decimal? RefundAmount { get; set; }
        public DateTime? RefundDate { get; set; }

        // Foreign Keys
        public int OrderId { get; set; }

        // Navigation Properties
        public virtual Order Order { get; set; }
    }
}
