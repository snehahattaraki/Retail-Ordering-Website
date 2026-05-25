namespace RetailOrdering.Infrastructure.Services
{
    public class PaymentGatewayService
    {
        public Task<bool> ProcessPaymentAsync(decimal amount)
        {
            return Task.FromResult(true);
        }
    }
}
