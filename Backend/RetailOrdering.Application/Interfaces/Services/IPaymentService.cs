using RetailOrdering.Application.DTOs.Payment;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto> GetByIdAsync(int id);
        Task<PaymentResponseDto> ProcessPaymentAsync(CreatePaymentDto createPaymentDto);
        Task<PaymentResponseDto> RefundPaymentAsync(int id);
        Task<string> GetStatusAsync(int id);
    }
}
