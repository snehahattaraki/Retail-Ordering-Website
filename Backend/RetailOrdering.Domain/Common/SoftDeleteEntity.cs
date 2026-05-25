namespace RetailOrdering.Domain.Common
{
    public abstract class SoftDeleteEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
