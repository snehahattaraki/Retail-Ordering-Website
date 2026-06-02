namespace RetailOrdering.Application.DTOs.Coupon;

public class CouponDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public decimal Discount { get; set; }
}
