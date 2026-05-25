using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class LoyaltyRepository : GenericRepository<LoyaltyPoint>, ILoyaltyRepository
    {
        public LoyaltyRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
