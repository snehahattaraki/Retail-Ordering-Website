using RetailOrdering.Application.DTOs.Promotion;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class PromotionService : IPromotionService
    {
        public Task<PromotionDto> CreateAsync(CreatePromotionDto createPromotionDto) => Task.FromResult(new PromotionDto());
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<PromotionDto>> GetActivePromotionsAsync() => Task.FromResult<IEnumerable<PromotionDto>>(new List<PromotionDto>());
        public Task<IEnumerable<PromotionDto>> GetAllAsync() => Task.FromResult<IEnumerable<PromotionDto>>(new List<PromotionDto>());
        public Task<PromotionDto> GetByIdAsync(int id) => Task.FromResult(new PromotionDto());
        public Task<PromotionDto> UpdateAsync(int id, UpdatePromotionDto updatePromotionDto) => Task.FromResult(new PromotionDto());
    }
}
