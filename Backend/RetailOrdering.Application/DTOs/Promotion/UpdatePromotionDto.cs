namespace RetailOrdering.Application.DTOs.Promotion
{
    public class UpdatePromotionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
    }
}
