using RetailOrdering.Domain.Common;

namespace RetailOrdering.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public Cart? Cart { get; set; }
    public LoyaltyPoint? LoyaltyPoint { get; set; }
}
