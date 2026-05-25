using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        // Navigation Properties
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
