using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Coupon;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly IGenericRepository<Domain.Entities.Coupon> _repo;

        public CouponService(IGenericRepository<Domain.Entities.Coupon> repo)
        {
            _repo = repo;
        }

        public async Task<CouponResponseDto?> ValidateAsync(string code)
        {
            var items = await _repo.FindAsync(c => c.Code == code && c.Expiry > System.DateTime.UtcNow);
            var c = System.Linq.Enumerable.FirstOrDefault(items);
            if (c == null) return null;
            return new CouponResponseDto(c.Id, c.Code, c.DiscountPercent, c.Expiry);
        }
    }
}
