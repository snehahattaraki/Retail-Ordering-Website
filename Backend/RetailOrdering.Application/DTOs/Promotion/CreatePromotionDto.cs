namespace RetailOrdering.Application.DTOs.Promotion
{
    public class CreatePromotionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
    }
}
