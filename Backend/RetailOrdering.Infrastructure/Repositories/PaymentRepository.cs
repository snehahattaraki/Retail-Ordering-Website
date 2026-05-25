using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Domain.Entities;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
