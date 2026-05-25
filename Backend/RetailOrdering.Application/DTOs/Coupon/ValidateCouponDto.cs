namespace RetailOrdering.Application.DTOs.Coupon
{
    public class ValidateCouponDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public decimal OrderAmount { get; set; }
    }
}
