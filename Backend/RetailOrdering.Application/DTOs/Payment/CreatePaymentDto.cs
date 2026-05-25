namespace RetailOrdering.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
