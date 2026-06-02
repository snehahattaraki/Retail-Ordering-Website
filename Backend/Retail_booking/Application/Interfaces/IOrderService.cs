using RetailOrdering.Application.DTOs.Order;

namespace RetailOrdering.Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
    Task<OrderDto?> GetOrderAsync(int orderId, int userId);
    Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(int orderId, int userId);
    Task<int> PlaceOrderAsync(int userId, PlaceOrderDto dto);
    Task<bool> ReorderAsync(int userId, int orderId);
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);
}