using RetailOrdering.Application.DTOs.Payment;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<PaymentResponseDto> ProcessPaymentAsync(CreatePaymentDto createPaymentDto) => Task.FromResult(new PaymentResponseDto());
        public Task<PaymentResponseDto> RefundPaymentAsync(int id) => Task.FromResult(new PaymentResponseDto());
        public Task<IEnumerable<PaymentDto>> GetAllAsync() => Task.FromResult<IEnumerable<PaymentDto>>(new List<PaymentDto>());
        public Task<PaymentDto> GetByIdAsync(int id) => Task.FromResult(new PaymentDto());
        public Task<string> GetStatusAsync(int id) => Task.FromResult(string.Empty);
    }
}
