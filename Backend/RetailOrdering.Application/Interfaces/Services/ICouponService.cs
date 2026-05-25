using RetailOrdering.Application.DTOs.Coupon;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetAllAsync();
        Task<CouponDto> GetByIdAsync(int id);
        Task<CouponDto> CreateAsync(CreateCouponDto createCouponDto);
        Task<CouponDto> UpdateAsync(int id, CreateCouponDto updateCouponDto);
        Task DeleteAsync(int id);
        Task<bool> ValidateCouponAsync(ValidateCouponDto validateCouponDto);
    }
}
