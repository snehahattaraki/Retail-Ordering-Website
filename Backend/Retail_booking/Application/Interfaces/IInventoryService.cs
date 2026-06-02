using RetailOrdering.Application.DTOs.Inventory;

namespace RetailOrdering.Application.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAllAsync();
    Task<InventoryDto?> GetByIdAsync(int id);
    Task<InventoryDto> CreateAsync(InventoryDto dto);
    Task<bool> UpdateAsync(int id, InventoryDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateStockAsync(int id, int stockQty);
}