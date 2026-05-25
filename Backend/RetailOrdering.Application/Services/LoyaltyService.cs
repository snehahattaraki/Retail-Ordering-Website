using RetailOrdering.Application.DTOs.Loyalty;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class LoyaltyService : ILoyaltyService
    {
        public Task AddPointsAsync(LoyaltyPointDto loyaltyPointDto) => Task.CompletedTask;
        public Task<IEnumerable<LoyaltyHistoryDto>> GetHistoryAsync(int userId) => Task.FromResult<IEnumerable<LoyaltyHistoryDto>>(new List<LoyaltyHistoryDto>());
        public Task<IEnumerable<LoyaltyPointDto>> GetUserPointsAsync(int userId) => Task.FromResult<IEnumerable<LoyaltyPointDto>>(new List<LoyaltyPointDto>());
        public Task RedeemPointsAsync(RedeemPointDto redeemPointDto) => Task.CompletedTask;
    }
}
