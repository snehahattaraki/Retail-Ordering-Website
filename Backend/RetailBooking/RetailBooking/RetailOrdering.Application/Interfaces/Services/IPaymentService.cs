using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Payment;

namespace RetailOrdering.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto);
    }
}
