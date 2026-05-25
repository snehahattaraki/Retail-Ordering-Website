using FluentValidation;
using RetailOrdering.Application.DTOs.Coupon;

namespace RetailOrdering.Application.Validators.Coupon
{
    public class CouponValidator : AbstractValidator<CreateCouponDto>
    {
        public CouponValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.DiscountPercent).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
