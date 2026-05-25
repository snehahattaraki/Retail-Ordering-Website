using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class EmailLog : AuditableEntity
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailType { get; set; }
        public bool IsSent { get; set; } = false;
        public string? ErrorMessage { get; set; }
        public int RetryCount { get; set; } = 0;
        public DateTime? SentDate { get; set; }

        // Foreign Keys
        public int? UserId { get; set; }
        public int? OrderId { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
    }
}
