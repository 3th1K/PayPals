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
            // Here, you can write custom logic to check if the username is not an integer.
            // You can use int.TryParse() to check if the string can be parsed as an integer.

            if (int.TryParse(username, out _))
            {
                return false; // Username is an integer
            }

            return true; // Username is not an integer
        }
    }
}
