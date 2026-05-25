namespace RetailOrdering.Application.DTOs.Loyalty
{
    public class LoyaltyHistoryDto
    {
        public DateTime Date { get; set; }
        public int Points { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
