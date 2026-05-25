using System;

namespace RetailOrdering.Application.DTOs.Payment
{
    public record PaymentResponseDto(Guid Id, Guid OrderId, decimal Amount, string Status, DateTime CreatedAt);
    public record CreatePaymentDto(Guid OrderId, decimal Amount, string Method);
}
