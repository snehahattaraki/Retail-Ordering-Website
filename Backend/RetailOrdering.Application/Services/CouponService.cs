using RetailOrdering.Application.DTOs.Coupon;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class CouponService : ICouponService
    {
        public Task<CouponDto> CreateAsync(CreateCouponDto createCouponDto) => Task.FromResult(new CouponDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<CouponDto>> GetAllAsync() => Task.FromResult<IEnumerable<CouponDto>>(new List<CouponDto>());
        public Task<CouponDto> GetByIdAsync(int id) => Task.FromResult(new CouponDto());
        public Task<bool> ValidateCouponAsync(ValidateCouponDto validateCouponDto) => Task.FromResult(true);
        public Task<CouponDto> UpdateAsync(int id, CreateCouponDto updateCouponDto) => Task.FromResult(new CouponDto());
    }
}
