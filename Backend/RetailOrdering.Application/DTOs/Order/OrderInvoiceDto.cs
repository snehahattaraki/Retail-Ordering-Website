namespace RetailOrdering.Application.DTOs.Order
{
    public class OrderInvoiceDto
    {
        public int OrderId { get; set; }
        public string InvoiceUrl { get; set; } = string.Empty;
    }
}
