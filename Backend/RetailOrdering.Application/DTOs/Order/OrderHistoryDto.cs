namespace RetailOrdering.Application.DTOs.Order
{
    public class OrderHistoryDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
