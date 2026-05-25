using RetailOrdering.Application.DTOs.Order;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto) => Task.FromResult(new OrderDto());
        public Task CancelOrderAsync(int id) => Task.CompletedTask;
        public Task<IEnumerable<OrderDto>> GetAllAsync() => Task.FromResult<IEnumerable<OrderDto>>(new List<OrderDto>());
        public Task<OrderDto> GetByIdAsync(int id) => Task.FromResult(new OrderDto());
        public Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId) => Task.FromResult<IEnumerable<OrderDto>>(new List<OrderDto>());
        public Task<OrderDto> UpdateStatusAsync(int id, UpdateOrderStatusDto updateOrderStatusDto) => Task.FromResult(new OrderDto());
    }
}
