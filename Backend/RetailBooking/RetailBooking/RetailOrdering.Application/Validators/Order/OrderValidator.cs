using FluentValidation;
using RetailOrdering.Application.DTOs.Order;

namespace RetailOrdering.Application.Validators.Order
{
    public class OrderValidator : AbstractValidator<CreateOrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Items).NotEmpty();
        }
    }
}
