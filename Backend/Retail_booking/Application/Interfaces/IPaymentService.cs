using RetailOrdering.Application.DTOs.Payment;

namespace RetailOrdering.Application.Interfaces;

public interface IPaymentService
{
    Task<bool> ProcessPaymentAsync(int orderId, PaymentDto dto);
}