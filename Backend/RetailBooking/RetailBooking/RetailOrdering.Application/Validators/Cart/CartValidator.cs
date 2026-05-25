using FluentValidation;
using RetailOrdering.Application.DTOs.Cart;

namespace RetailOrdering.Application.Validators.Cart
{
    public class CartValidator : AbstractValidator<AddCartItemDto>
    {
        public CartValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
