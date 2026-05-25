using RetailOrdering.Application.DTOs.Order;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId);
        Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> UpdateStatusAsync(int id, UpdateOrderStatusDto updateOrderStatusDto);
        Task CancelOrderAsync(int id);
    }
}
