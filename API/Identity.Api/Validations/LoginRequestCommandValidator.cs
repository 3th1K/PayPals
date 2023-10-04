using FluentValidation;
using Identity.Api.Commands;

namespace Identity.Api.Validations
{
    public class LoginRequestCommandValidator : AbstractValidator<LoginRequestCommand>
    {
        public LoginRequestCommandValidator()
        {
            RuleFor(x => x.Username)
                .MinimumLength(5).WithMessage("Username Must be minimum length of 5")
                .Must(BeAValidUsername).WithMessage("Username cannot be an integer");
        }
        private bool BeAValidUsername(string username)
        {

            if (int.TryParse(username, out _))
            {
                return false; // Username is an integer
            }

            return true; // Username is not an integer
        }
    }
}
