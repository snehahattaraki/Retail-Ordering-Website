using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class PackagingRepository : GenericRepository<Packaging>, IPackagingRepository
    {
        public PackagingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
