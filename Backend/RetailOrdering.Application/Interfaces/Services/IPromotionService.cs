using RetailOrdering.Application.DTOs.Promotion;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionDto>> GetAllAsync();
        Task<PromotionDto> GetByIdAsync(int id);
        Task<IEnumerable<PromotionDto>> GetActivePromotionsAsync();
        Task<PromotionDto> CreateAsync(CreatePromotionDto createPromotionDto);
        Task<PromotionDto> UpdateAsync(int id, UpdatePromotionDto updatePromotionDto);
        Task DeleteAsync(int id);
    }
}
