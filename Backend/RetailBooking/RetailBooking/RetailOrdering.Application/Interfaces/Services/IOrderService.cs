using System;
using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Order;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto> PlaceOrderAsync(CreateOrderDto dto);
        Task<OrderResponseDto> GetByIdAsync(Guid id);
    }
}
