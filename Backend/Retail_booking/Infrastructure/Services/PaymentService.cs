using Microsoft.EntityFrameworkCore;
using RetailOrdering.Application.DTOs.Payment;
using RetailOrdering.Application.Interfaces;
using RetailOrdering.Infrastructure.Data;

namespace RetailOrdering.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ProcessPaymentAsync(int orderId, PaymentDto dto)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;
        var payment = new RetailOrdering.Domain.Entities.Payment
        {
            OrderId = orderId,
            Amount = dto.Amount,
            Status = "Completed"
        };
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return true;
    }
}