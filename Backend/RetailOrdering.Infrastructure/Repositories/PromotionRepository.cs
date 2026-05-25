using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
