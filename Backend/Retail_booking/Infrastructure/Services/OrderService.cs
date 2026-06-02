using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Order;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;
using AutoMapper;

namespace RetailOrdering.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _context.Orders.Where(o => o.UserId == userId).Include(o => o.OrderItems).ToListAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderAsync(int orderId, int userId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        if (order == null) return null;
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(int orderId, int userId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        if (order == null) return Enumerable.Empty<OrderItemDto>();
        return _mapper.Map<IEnumerable<OrderItemDto>>(order.OrderItems);
    }

    public async Task<int> PlaceOrderAsync(int userId, PlaceOrderDto dto)
    {
        var cartItems = dto.Items;
        // validate stock
        foreach (var it in cartItems)
        {
            var product = await _context.Products.FindAsync(it.ProductId);
            if (product == null) throw new InvalidOperationException($"Product {it.ProductId} not found");
            if (product.StockQty < it.Quantity) throw new InvalidOperationException($"Product {product.Name} is out of stock");
        }

        var order = new RetailOrdering.Domain.Entities.Order
        {
            UserId = userId,
            Status = RetailOrdering.Domain.Entities.OrderStatus.Confirmed,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = 0m
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        decimal total = 0m;
        foreach (var it in cartItems)
        {
            var product = await _context.Products.FindAsync(it.ProductId);
            var oi = new RetailOrdering.Domain.Entities.OrderItem
            {
                OrderId = order.Id,
                ProductId = it.ProductId,
                Quantity = it.Quantity,
                Price = product.Price
            };
            total += oi.Price * oi.Quantity;
            await _context.OrderItems.AddAsync(oi);
            product.StockQty -= it.Quantity;
            _context.Products.Update(product);
        }

        order.TotalAmount = total;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<bool> ReorderAsync(int userId, int orderId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        if (order == null) return false;
        var newOrder = new RetailOrdering.Domain.Entities.Order
        {
            UserId = userId,
            Status = RetailOrdering.Domain.Entities.OrderStatus.Confirmed,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = order.TotalAmount
        };
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        foreach (var oi in order.OrderItems)
        {
            var newOi = new RetailOrdering.Domain.Entities.OrderItem
            {
                OrderId = newOrder.Id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            };
            await _context.OrderItems.AddAsync(newOi);
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;
        if (Enum.TryParse<RetailOrdering.Domain.Entities.OrderStatus>(status, out var s))
        {
            order.Status = s;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}