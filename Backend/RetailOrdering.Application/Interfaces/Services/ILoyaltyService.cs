using RetailOrdering.Application.DTOs.Loyalty;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface ILoyaltyService
    {
        Task<IEnumerable<LoyaltyPointDto>> GetUserPointsAsync(int userId);
        Task<IEnumerable<LoyaltyHistoryDto>> GetHistoryAsync(int userId);
        Task AddPointsAsync(LoyaltyPointDto loyaltyPointDto);
        Task RedeemPointsAsync(RedeemPointDto redeemPointDto);
    }
}
