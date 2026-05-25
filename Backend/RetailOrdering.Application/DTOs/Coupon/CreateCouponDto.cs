namespace RetailOrdering.Application.DTOs.Coupon
{
    public class CreateCouponDto
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
    }
}
