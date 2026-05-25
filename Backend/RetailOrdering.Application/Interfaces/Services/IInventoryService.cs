using RetailOrdering.Application.DTOs.Inventory;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync();
        Task<InventoryDto> GetByProductIdAsync(int productId);
        Task<InventoryDto> UpdateAsync(int id, UpdateInventoryDto updateInventoryDto);
        Task<bool> CheckStockAsync(StockValidationDto validationDto);
        Task<IEnumerable<InventoryDto>> GetLowStockAsync();
    }
}
