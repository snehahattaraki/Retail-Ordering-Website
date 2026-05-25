using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RetailOrdering.Application.DTOs.Order;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Domain.Entities.Order> _orderRepo;
        private readonly IGenericRepository<Domain.Entities.Inventory> _inventoryRepo;
        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Domain.Entities.Order> orderRepo, IGenericRepository<Domain.Entities.Inventory> inventoryRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto> GetByIdAsync(Guid id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found");
            return new OrderResponseDto(order.Id, order.UserId, order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice)), order.Total, order.CreatedAt, order.Status);
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(CreateOrderDto dto)
        {
            // Basic stock validation
            foreach (var item in dto.Items)
            {
                var inv = await _inventoryRepo.GetByIdAsync(item.ProductId);
                if (inv == null || inv.Quantity < item.Quantity) throw new Exception($"Insufficient stock for product {item.ProductId}");
            }

            var order = new Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = "Placed",
                Items = dto.Items.Select(i => new Domain.Entities.OrderItem { Id = Guid.NewGuid(), ProductId = i.ProductId, Quantity = i.Quantity, UnitPrice = i.UnitPrice }).ToList()
            };
            order.Total = order.Items.Sum(i => i.Quantity * i.UnitPrice);
            await _orderRepo.AddAsync(order);

            // Deduct inventory
            foreach (var item in order.Items)
            {
                var inv = await _inventoryRepo.GetByIdAsync(item.ProductId);
                if (inv != null) { inv.Quantity -= item.Quantity; await _inventoryRepo.UpdateAsync(inv); }
            }

            return new OrderResponseDto(order.Id, order.UserId, order.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity, i.UnitPrice)), order.Total, order.CreatedAt, order.Status);
        }
    }
}
