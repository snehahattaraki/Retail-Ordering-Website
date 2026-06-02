using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public class EmailLog : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
