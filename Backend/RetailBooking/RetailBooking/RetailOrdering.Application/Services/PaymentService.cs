using System.Threading.Tasks;
using RetailOrdering.Application.DTOs.Payment;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Application.Interfaces.Services;

namespace RetailOrdering.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<Domain.Entities.Payment> _repo;

        public PaymentService(IGenericRepository<Domain.Entities.Payment> repo)
        {
            _repo = repo;
        }

        public async Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var p = new Domain.Entities.Payment { Id = System.Guid.NewGuid(), OrderId = dto.OrderId, Amount = dto.Amount, Status = "Created", CreatedAt = System.DateTime.UtcNow };
            await _repo.AddAsync(p);
            return new PaymentResponseDto(p.Id, p.OrderId, p.Amount, p.Status, p.CreatedAt);
        }
    }
}
