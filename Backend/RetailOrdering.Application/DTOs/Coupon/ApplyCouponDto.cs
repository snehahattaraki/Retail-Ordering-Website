namespace RetailOrdering.Application.DTOs.Coupon
{
    public class ApplyCouponDto
    {
        public int OrderId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
    }
}
