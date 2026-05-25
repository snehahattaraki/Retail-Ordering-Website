using FluentValidation;
using RetailOrdering.Application.DTOs.Auth;

namespace RetailOrdering.Application.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
