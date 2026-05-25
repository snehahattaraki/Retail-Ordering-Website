using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
