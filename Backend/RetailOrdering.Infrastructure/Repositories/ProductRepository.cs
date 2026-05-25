using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
