using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Coupon;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface ICouponService
    {
        Task<CouponResponseDto?> ValidateAsync(string code);
    }
}
