using RetailOrdering.Application.DTOs.Inventory;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class InventoryService : IInventoryService
    {
        public Task<IEnumerable<InventoryDto>> GetAllAsync() => Task.FromResult<IEnumerable<InventoryDto>>(new List<InventoryDto>());
        public Task<InventoryDto> GetByProductIdAsync(int productId) => Task.FromResult(new InventoryDto());
        public Task<InventoryDto> UpdateAsync(int id, UpdateInventoryDto updateInventoryDto) => Task.FromResult(new InventoryDto());
        public Task<bool> CheckStockAsync(StockValidationDto validationDto) => Task.FromResult(true);
        public Task<IEnumerable<InventoryDto>> GetLowStockAsync() => Task.FromResult<IEnumerable<InventoryDto>>(new List<InventoryDto>());
    }
}
